using System;
using System.ComponentModel.DataAnnotations;

namespace PAX.Models
{
    public class Generic
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Generic()
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
    }
}