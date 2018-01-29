using System;
using Vidly_Udemy.Models;
using FizzWare.NBuilder;
namespace Vidly_Udemy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Vidly_Udemy.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Vidly_Udemy.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            // Create test datas with nBuilder and Faker.NET
            Random rnd = new Random();
            var customers = Builder<Customer>.CreateListOfSize(50)
                .All()
                    .With(c => c.Name = Faker.Name.FullName())
                    .With(c => c.IsSubscribedToNewsletter = rnd.Next(100) <= 50 ? true : false)
                    .With(c => c.MembershipTypeId = (byte)Faker.RandomNumber.Next(1, 5))
                    .With(c => c.BirthDate = null)
                    .With(c => c.BirthDate = RandomDay(rnd))
                .Build();

            context.Customers.AddOrUpdate(c => c.Id, customers.ToArray());
            
        }

        private DateTime RandomDay(Random rnd)
        {
            DateTime start = new DateTime(1965, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
