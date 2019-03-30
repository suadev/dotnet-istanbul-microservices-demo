using System;
using System.Reflection;
using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using RawRabbit;
using RawRabbit.Common;

namespace Shared.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _defaultNamespace;
        private readonly int _retries;
        private readonly int _retryInterval;

        public BusSubscriber(IApplicationBuilder app)
        {
            _logger = app.ApplicationServices.GetService<ILogger<BusSubscriber>>();
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();

            var options = _serviceProvider.GetService<RabbitMqOptions>();
            _defaultNamespace = options.Namespace;
            _retries = options.Retries >= 0 ? options.Retries : 3;
            _retryInterval = options.RetryInterval > 0 ? options.RetryInterval : 2;
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string _namespace = null)
            where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
                {
                    var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                    return await TryHandleAsync(command, correlationContext,
                        () => commandHandler.HandleAsync(command, correlationContext));
                },
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>(_namespace)))));

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(string _namespace = null)
            where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent, CorrelationContext>(async (_event, correlationContext) =>
                {
                    var eventHandler = _serviceProvider.GetService<IEventHandler<TEvent>>();

                    return await TryHandleAsync(_event, correlationContext,
                        () => eventHandler.HandleAsync(_event, correlationContext));
                },
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>(_namespace)))));

            return this;
        }

        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message,
            CorrelationContext correlationContext, Func<Task> handle)
        {
            var currentRetry = 0;
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retries, i => TimeSpan.FromSeconds(_retryInterval));

            var messageName = message.GetType().Name;

            return await retryPolicy.ExecuteAsync<Acknowledgement>(async () =>
            {
                var retryMessage = currentRetry == 0 ? string.Empty : $"Retry: {currentRetry}'.";
                var messageType = message is IEvent ? "n event" : " command";

                _logger.LogInformation($"[Handled a{messageType}] : '{messageName}' " +
                                     $"Correlation id: '{correlationContext.CorrelationId}'. {retryMessage}");
                await handle();

                return new Ack();
            });
        }

        private string GetQueueName<T>(string _namespace = null)
        {
            _namespace = string.IsNullOrWhiteSpace(_namespace)
                ? (string.IsNullOrWhiteSpace(_defaultNamespace) ? string.Empty : _defaultNamespace)
                : _namespace;
            var separatedNamespace = string.IsNullOrWhiteSpace(_namespace) ? string.Empty : $"{_namespace}.";
            return $"{Assembly.GetEntryAssembly().GetName().Name}/{separatedNamespace}{typeof(T).Name.Underscore()}";
        }
    }
}