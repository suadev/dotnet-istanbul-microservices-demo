using System;

namespace Shared.Messages
{
    public class MessageNamespaceAttribute : Attribute
    {
        public string Namespace { get; }

        public MessageNamespaceAttribute(string _namespace)
        {
            Namespace = _namespace?.ToLowerInvariant();
        }
    }
}