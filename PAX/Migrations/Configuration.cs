using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PAX.Models;
using PAX.Services;

namespace PAX.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PAX.Models.ConnectionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ConnectionContext context)
        {
            base.Seed(context);

            var sam = new Profile() { Name = "Sam", Location = "Anderlecht"};
            var ryan = new Profile() { Name = "Ryan", Location = "Brussels"};

            var item = new Item()
            {
                Profile = ryan,
                Name = "Dark Souls 2 CD",
                Price = 11.5m,
                Pictures = new List<Picture>
                {
                    new Picture() { }
                }
            };

            ryan.Items = new List<Item> { item };
            sam.Offers = new List<Offer> { new Offer() { Item = item, Price = 9.00m } };

            var _repo = new AuthRepository(context);
            _repo.Create(new IdentityUser(), sam);
            _repo.Create(new IdentityUser(), ryan);


            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        //sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
            }
        }

    }
}
