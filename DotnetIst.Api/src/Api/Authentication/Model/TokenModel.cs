using System;

namespace Api.Authentication
{
    public class TokenModel
    {
        public Guid CustomerId { get; set; }
        public string Email { get; set; }
    }
}