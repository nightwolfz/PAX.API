using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PAX.Models;

namespace PAX.Services
{
    public class ProfileRepository : GenericRepository<Profile>
    {
        public ProfileRepository(ConnectionContext db): base(db)
        {
        }

        public async virtual Task<List<Profile>> GetAll(int number = 5)
        {
            var query = from profile in db.Profiles.Include(d => d.Offers).Include(d => d.Items) select profile;
            return await query.Take(number).ToListAsync();
        }

        public virtual bool Exists(string id)
        {
            return db.Profiles.Count(e => e.ProfileId == id) > 0;
        }
    }
}