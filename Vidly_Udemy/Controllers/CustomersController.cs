using BootstrapMvc.Lists;
using System.Collections.Generic;
using System.Web.Mvc;
using Vidly_Udemy.Models;
using System.Linq;

namespace Vidly_Udemy.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ViewResult Index()
        {
            var customers = GetCustomers();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Hanry Avery" },
                new Customer { Id = 2, Name = "Francis Drake" }
            };
        }
    }
}