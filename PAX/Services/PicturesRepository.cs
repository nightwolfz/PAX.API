using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PAX.Models;

namespace PAX.Services
{
    public class PicturesRepository : GenericRepository<Profile>
    {
        public PicturesRepository(ConnectionContext db): base(db)
        {
        }

        public async Task Add(Item item, string src)
        {
            db.Pictures.Add(new Picture()
            {
                Item = item,
                Src = src
            });
            await db.SaveChangesAsync();
        }

        public async virtual Task<List<Picture>> GetAll(int number = 5)
        {
            var query = from profile in db.Pictures
                        select profile;
            return await query.Take(number).ToListAsync();
        }

        public virtual bool Exists(string id)
        {
            return db.Pictures.Count(e => e.PictureId == id) > 0;
        }
    }
}