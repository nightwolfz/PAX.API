﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using PAX.Models;
using PAX.Services;
using PAX.Helpers;
using Picture = PAX.Models.Picture;

namespace PAX.Controllers
{
    public class PicturesController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        public async Task<List<Picture>> GetPictures()
        {
            return await db.Pictures.GetAll();
        }

        public async Task<IHttpActionResult> GetPicture(string id)
        {
            var picture = await db.Pictures.Find(id);
            if (picture == null) return NotFound();
            
            return Ok(picture);
        }

        public async Task<IHttpActionResult> PostPicture(string itemId)
        {
            Item item = await db.Items.FindOne(i => i.Id == itemId);
            //Item item = await db.Items.Find(itemId);

            if (item == null) return BadRequest("Id not found.");
            

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Unsupported Media Type. Must be multipart/form-data. RECEIVED: " + Request.Content.Headers.ContentType);
            }

            var root = HttpContext.Current.Server.MapPath(Helpers.Picture.uploadFolder);
            var provider = new MultipartFormDataStreamProvider(root);

            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);

            // This illustrates how to get the file names.
            var messages = new List<Picture>();
            foreach (var file in provider.FileData)
            {
                var picture = new Picture();
                item.Pictures.Add(picture);
                messages.Add(picture);
                Helpers.Picture.Resize(file.LocalFileName, picture.Id);
            }
            await db.SaveChanges();
            return Ok(messages);
        }

        public async Task<IHttpActionResult> DeletePicture(string id)
        {
            var picture = await db.Pictures.Find(id);
            if (picture == null) return NotFound();

            db.Pictures.Delete(picture);
            await db.SaveChanges();

            return Ok(picture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}