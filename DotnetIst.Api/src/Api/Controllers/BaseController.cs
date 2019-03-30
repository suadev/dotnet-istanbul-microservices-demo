using System;
using Api.Authentication;
using Shared.Messages;
using Shared.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IBusPublisher BusPublisher;
        public BaseController(IBusPublisher busPublisher)
        {
            BusPublisher = busPublisher;
        }

        protected TokenModel CurrentUser
        {
            get
            {
                return HttpContext.Items["CurrentCustomer"] != null ?
                    HttpContext.Items["CurrentCustomer"] as TokenModel : null;
            }
        }

        protected ICorrelationContext GetContext()
        {
            return GetContext(CurrentUser.CustomerId);
        }

        //This method is only for AllowAnonymus CustomerController
        protected ICorrelationContext GetContext(Guid customerId)
        {
            return CorrelationContext.Create(Guid.NewGuid(), customerId);
        }

        // protected IActionResult Accepted(ICorrelationContext context)
        // {        //     
        //     // Response.Headers.Add(OperationHeader, $"checktransactionstatus/{context.Id}");
        //     return base.Accepted();
        // }
    }
}