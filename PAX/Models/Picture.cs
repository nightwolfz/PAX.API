using System.ComponentModel.DataAnnotations;

namespace PAX.Models
{
    public class Picture : Generic
    {
        public string ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}