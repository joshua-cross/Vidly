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

            //setting the view model to be the list of customers we just made.
            var viewModel = new CustomerViewModel
            {
                Customers = getCustomers()
            };



            return View(viewModel);
        }


        //getting the customer based on what ID has been sent through.
        [Route("Customers/Detail/{id}")]
        public ActionResult Detail(int id)
        {
            try
            {
                var customer = new Customer();
                customer = getCustomers()[id];
                return View(customer);
            } catch
            {
                return HttpNotFound();
            }
        }
        //a class that creates a list of customers then returns it to be given to CustomerViewModel.
        public List<Customer> getCustomers()
        {

            var customers = new List<Customer>
        {
            new Customer { Name = "Geoff Smith", Id = 1 },
            new Customer { Name = "Bunjamin Button", Id = 2 },
            new Customer { Name = "Frank Skinner", Id = 3 },
            new Customer { Name = "Betty Spaghetti", Id = 4 }
        };



            return customers;
        }


    }

}