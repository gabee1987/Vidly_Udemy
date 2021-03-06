﻿using BootstrapMvc.Lists;
using System.Collections.Generic;
using System.Web.Mvc;
using Vidly_Udemy.Models;
using System.Linq;
using System.Data.Entity;

namespace Vidly_Udemy.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(ctor => ctor.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // Get customers without database
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "Hanry Avery" },
        //        new Customer { Id = 2, Name = "Francis Drake" }
        //    };
        //}
    }
}