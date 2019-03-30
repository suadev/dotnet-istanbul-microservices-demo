
using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Customers.Commands
{
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public CreateCustomer(Guid id, string email, string password, string firstName, string lastName, string address)
        {
            this.Id = id;
            this.Password = password;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
        }
    }
}