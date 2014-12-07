using System.Collections.Generic;

namespace PAX.Models
{
    public class Profile : Generic
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int GoodTrades { get; set; }
        public int BadTrades { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        //[JsonIgnore]public ICollection<Category> Categories { get; set; }

        // Gets the trade success ratio
        public int GetTradeRatio()
        {
            return 1 - (BadTrades/GoodTrades)/(BadTrades + GoodTrades)*100;
        }
    }
}