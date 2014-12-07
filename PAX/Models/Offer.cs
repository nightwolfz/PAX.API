using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PAX.Models
{
    public class Offer : Generic
    {
        public string ProfileId { get; set; }
        public string ItemId { get; set; }
        [Required]public decimal Price { get; set; }

        [JsonIgnore]public virtual Profile Profile { get; set; }
        [JsonIgnore]public virtual Item Item { get; set; }
    }
}