using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PAX.Models;

namespace PAX.Services
{
    public class UnitOfWork : IDisposable
    {
        ConnectionContext _context = new ConnectionContext();

        ProfileRepository _profile;
        public ProfileRepository Profiles => _profile ?? (_profile = new ProfileRepository(_context));

        PicturesRepository _picture;
        public PicturesRepository Pictures => _picture ?? (_picture = new PicturesRepository(_context));

        ItemsRepository _item;
        public ItemsRepository Items => _item ?? (_item = new ItemsRepository(_context));

        public async Task SaveChanges() => await _context.SaveChangesAsync();
       

        // Release ressources
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}