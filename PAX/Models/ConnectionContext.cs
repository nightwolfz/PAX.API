using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PAX.Models
{

    public class ConnectionContext : IdentityDbContext<IdentityUser>
    {
        public ConnectionContext() : base("DefaultConnection")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>().HasMany(s => s.Items).WithRequired(s => s.Profile).HasForeignKey(s => s.ProfileId);
            modelBuilder.Entity<Profile>().HasMany(s => s.Offers).WithRequired(s => s.Profile).HasForeignKey(s => s.ProfileId);

            modelBuilder.Entity<Item>().HasMany(s => s.Pictures).WithRequired(s => s.Item).HasForeignKey(s => s.ItemId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Item>().HasMany(s => s.Offers).WithRequired(s => s.Item).HasForeignKey(s => s.ItemId).WillCascadeOnDelete(false);
        }
    }

}
