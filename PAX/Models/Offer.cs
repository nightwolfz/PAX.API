using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PAX.Models
{
    public class Offer
    {
        public Offer()
        {
            OfferId = Guid.NewGuid().ToString();
        }

        [Key]
        public string OfferId { get; set; }
        public string ProfileId { get; set; }
        public string ItemId { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }
    }
}