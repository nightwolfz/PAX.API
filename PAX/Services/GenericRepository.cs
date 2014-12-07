using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PAX.Models;
using EntityState = System.Data.Entity.EntityState;

namespace PAX.Services
{
    public class GenericRepository<T> where T : class
    {
        internal ConnectionContext db;
        internal DbSet<T> DbSet;

        public GenericRepository(ConnectionContext db)
        {
            this.db = db;
            this.DbSet = db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Find(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> FindOne(Expression<Func<T, bool>> filter) => await DbSet.SingleOrDefaultAsync(filter);

        public virtual async Task<T> Find(object id) => await DbSet.FindAsync(id);

        public virtual void Add(T entity) => DbSet.Add(entity);
        
        public virtual async void Delete(object id) => Delete(await DbSet.FindAsync(id));
        

        public virtual void Delete(T entityToDelete)
        {
            if (db.Entry(entityToDelete).State == EntityState.Detached) DbSet.Attach(entityToDelete);
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}