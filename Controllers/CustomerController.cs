using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Customers;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Route("Customers")]
        public ActionResult Index()
        {

            //creating a list of customers.
            var customers = new List<Customer>
            {
                new Customer { Name = "Geoff Smith", Id = 1 },
                new Customer { Name = "Bunjamin Button", Id = 2 },
                new Customer { Name = "Frank Skinner", Id = 3 },
                new Customer { Name = "Betty Spaghetti", Id = 4 }
            };

            //setting the view model to be the list of customers we just made.
            var viewModel = new CustomerViewModel
            {
                Customers = customers
            };



            return View(viewModel);
        }
    }
}