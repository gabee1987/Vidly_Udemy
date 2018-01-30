using System.IO;
using BootstrapMvc.Lists;
using System;
using Vidly_Udemy.Models;
using FizzWare.NBuilder;
namespace Vidly_Udemy.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;

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

            List<Genre> genreListToPopulateDB = GetGenres();
            var movies = Builder<Movie>.CreateListOfSize(50)
                .All()
                    .With(m => m.Name = GetRandomMovieTitle(rnd))
                    .With(m => m.ReleaseDate = RandomDay(rnd))
                    .With(m => m.DateAdded = DateTime.Today)
                    .With(m => m.Genre = genreListToPopulateDB[rnd.Next(genreListToPopulateDB.Count)])
                    .With(m => m.GenreId = (byte)rnd.Next(1, 9))
                    .With(m => m.NumberInStock = (byte)rnd.Next(1, 30))
                    .Build();

            context.Movies.AddOrUpdate(m => m.Id, movies.ToArray());

            //var movies = Builder<Movie>.CreateListOfSize(50)
            //    .All()
            //        .With(m => m.Name = GetRandomMovieTitle(rnd))
            //        .With(m => m.ReleaseDate = RandomDay(rnd))
            //        .With(m => m.DateAdded = DateTime.Today)
            //        .With(m => m.Genre = GetRandomGenre(rnd))
            //    .Build();

            //context.Movies.AddOrUpdate(m => m.Id, movies.ToArray());

        }

        private DateTime RandomDay(Random rnd)
        {
            DateTime start = new DateTime(1965, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        private string GetRandomMovieTitle(Random rnd)
        {
            //string pathToMovieTitles = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"MovieTitles\movie_list.txt");
            string pathToMovieTitles = @"I:\CodeCool\.NET\ASP.NET\Vidly_Udemy\Vidly_Udemy\MovieTitles\movie_list_onlyTitles.txt";
            var tempTitles = File.ReadAllLines(pathToMovieTitles);
            List<string> movieTitles = new List<string>(tempTitles);
            return movieTitles[rnd.Next(movieTitles.Count)];
        }

        private List<Genre> GetGenres()
        {
            List<Genre> tempGenreList = new List<Genre>();
            List<string> genres = new List<string>
            {
                "Action", "Thriller", "Family", "Romance", "Comedy", "Drama", "Horror", "Documentary"
            };
            for (int i = 0; i < genres.Count; i++)
            {
                Genre genre = new Genre();
                genre.Name = genres[i];
                tempGenreList.Add(genre);
            }
            //Genre tempGenre = new Genre();
            //tempGenre.Name = genres[rnd.Next(genres.Count)];

            return tempGenreList;
        }
    }
}
