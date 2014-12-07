using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PAX.Models;

namespace PAX.Controllers
{
    public class ItemsController : ApiController
    {
        private ConnectionContext db = new ConnectionContext();

        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        public async Task<IHttpActionResult> GetItem(string id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        public async Task<IHttpActionResult> PutItem(string id, Item item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != item.Id) return BadRequest("Incorrect id");
            if (!ItemExists(id)) return NotFound();

            item.UpdatedOn = DateTime.Now;
            db.Entry(item).State = EntityState.Modified;

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        public async Task<IHttpActionResult> PostItem(Item item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Items.Add(item);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        public async Task<IHttpActionResult> DeleteItem(string id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null) return NotFound();

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        private bool ItemExists(string id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}