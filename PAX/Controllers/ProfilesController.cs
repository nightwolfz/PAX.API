using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PAX.Models;
using PAX.Services;

namespace PAX.Controllers
{
    public class ProfilesController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: api/Profiles
        public async Task<List<Profile>> GetProfiles()
        {
            return await db.Profiles.GetAll();
        }

        // GET: api/Profiles/5
        public async Task<IHttpActionResult> GetProfile(string id)
        {
            var profile = await db.Profiles.Find(id);
            if (profile == null) return NotFound();
            
            return Ok(profile);
        }

        public async Task<IHttpActionResult> PutProfile(string id, Profile profile)
        {
            if (!ModelState.IsValid) return BadRequest("Model state sucks");
            if (id != profile.ProfileId) return BadRequest("Cannot change GUID");

            await db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> PostProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            await db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = profile.ProfileId }, profile);
        }

        public async Task<IHttpActionResult> DeleteProfile(string id)
        {
            var profile = await db.Profiles.Find(id);
            if (profile == null) return NotFound();

            db.Profiles.Delete(profile);
            await db.SaveChanges();

            return Ok(profile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}