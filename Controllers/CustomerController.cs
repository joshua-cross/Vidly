using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Customers;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {

        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        [Route("Customers")]
        public ActionResult Index()
        {

            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            try
            {
                List<Customer> customerList = new List<Customer>();
            } catch
            {

            }

            //setting the view model to be the list of customers we just made.
            var viewModel = new CustomerViewModel
            {
                Customers = getCustomers()
            };



            return View(customers);
        }


        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            //saving the changes that was made to the local memory.
            _context.Customers.Add(customer);
            //saving the changes that was made to the database.
            _context.SaveChanges();

            //redirecting the user away from this page.
            return RedirectToAction("Index", "Customers");
        }

        //getting the customer based on what ID has been sent through.
        [Route("Customers/Detail/{id}")]
        public ActionResult Detail(int id)
        {
            try
            {
                var customer = new Customer();
                customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
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