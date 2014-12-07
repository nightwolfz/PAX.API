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
    public class OffersController : ApiController
    {
        private ConnectionContext db = new ConnectionContext();

        public IQueryable<Offer> GetOffers()
        {
            return db.Offers;
        }

        public async Task<IHttpActionResult> GetOffer(string id)
        {
            Offer Offer = await db.Offers.FindAsync(id);
            if (Offer == null) return NotFound();

            return Ok(Offer);
        }

        public async Task<IHttpActionResult> PutOffer(string id, Offer Offer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != Offer.Id) return BadRequest("Incorrect id");
            if (!OfferExists(id)) return NotFound();

            Offer.UpdatedOn = DateTime.Now;
            db.Entry(Offer).State = EntityState.Modified;

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        public async Task<IHttpActionResult> PostOffer(Offer Offer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Offers.Add(Offer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Offer.Id }, Offer);
        }

        public async Task<IHttpActionResult> DeleteOffer(string id)
        {
            Offer Offer = await db.Offers.FindAsync(id);
            if (Offer == null) return NotFound();

            db.Offers.Remove(Offer);
            await db.SaveChangesAsync();

            return Ok(Offer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        private bool OfferExists(string id)
        {
            return db.Offers.Count(e => e.Id == id) > 0;
        }
    }
}