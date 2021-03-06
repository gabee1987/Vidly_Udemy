﻿using Vidly_Udemy.ViewModels;
using System.Data.Entity.Infrastructure.MappingViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly_Udemy.Models;
using System.Data.Entity;

namespace Vidly_Udemy.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();    
        }


        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }




















        //public ViewResult Index()
        //{
        //    var movies = GetMovies();

        //    return View(movies);
        //}

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Blade Runner 2049"},
        //        new Movie { Id = 2, Name = "Star Wars - The Last Jedi"}
        //    };
        //}


        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Inception" };
        //    var customers = new List<Customer>
        //    {
        //        new Customer { Name = "Customer1" },
        //        new Customer { Name = "Customer2" }
        //    };


        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };
        //    return View(viewModel);
        //}









        // GET: Movies
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Star Wars" };
        //    var movie1 = new Movie() { Name = "Blade Runner 2049" };
        //    var customers = new List<Customer>
        //    {
        //        new Customer { Name = "Customer1" },
        //        new Customer { Name = "Customer2" },
        //        new Customer { Name = "Customer2" }
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };
        //    return View(viewModel);
        //}

        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id); 
        //}

        //// movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}


        // With attribute routing, new modern way
        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseYear(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}


        //// Without attribute routing, old convensional way
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}