using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PAX.Models
{
    public class Item : Generic
    {
        public Item()
        {
            Available = true;
        }

        public string ProfileId { get; set; }
        [Required]public string Name { get; set; }

        public decimal Price { get; set; }
        public bool Available { get; set; }

        [JsonIgnore]public virtual ICollection<Picture> Pictures { get; set; }
        [JsonIgnore]public virtual ICollection<Offer> Offers { get; set; }
        [JsonIgnore]public virtual Profile Profile { get; set; }
        //[JsonIgnore]public ICollection<Category> Categories { get; set; }
    }
}