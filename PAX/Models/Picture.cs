using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PAX.Models
{
    public class ImageMedia
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public byte[] Buffer { get; set; }

        public ImageMedia(string fileName, string mediaType, byte[] imagebuffer)
        {
            FileName = fileName;
            MediaType = mediaType;
            Buffer = imagebuffer;
        }
    }

    public class Picture
    {
        public Picture()
        {
            PictureId = Guid.NewGuid().ToString();
        }

        [Key]
        public string PictureId { get; set; }
        public string ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}