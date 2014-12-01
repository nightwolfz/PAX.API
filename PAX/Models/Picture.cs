using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PAX.Models
{
    public class Picture
    {
        public Picture()
        {
            PictureId = Guid.NewGuid().ToString();
        }

        [Key]
        public string PictureId { get; set; }
        public string ItemId { get; set; }
        public string Src { get; set; }

        public virtual Item Item { get; set; }
    }
}