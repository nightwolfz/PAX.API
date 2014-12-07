using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PAX.Models;

namespace PAX.Services
{
    public class ItemsRepository : GenericRepository<Item>
    {
        public ItemsRepository(ConnectionContext db): base(db)
        {
        }

        public virtual bool Exists(string id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}