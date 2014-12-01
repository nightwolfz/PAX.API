using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAX.Models
{
    public class Profile
    {
        public Profile()
        {
            SignedUp = DateTime.Now;
        }

        [Key]
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime SignedUp { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<Offer> Offers { get; set; }
        //public ICollection<Category> Categories { get; set; }
    }
}