using System;

namespace Shared.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public BaseEntity()
        {
            this.CreateDate = DateTime.Now;
        }
    }
}