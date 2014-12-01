using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PAX.Models
{
    public class Item
    {
        public Item()
        {
            ItemId = Guid.NewGuid().ToString();
            CreatedOn = DateTime.Now;
            Available = true;
        }

        [Key]
        public string ItemId { get; set; }
        public string ProfileId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedOn { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        //public ICollection<Category> Categories { get; set; }
    }
}