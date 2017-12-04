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
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {

            /*If what the user has entered is not valid we will take the user back to the form*/
            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("New", viewModel);
            }

            //checking if the customer was edited or created, if it was created then it will have an Id of 0, else it's edited.
            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            //element aready exists.
            else
            {

                //getting element from the database; using single here not singleOrDefault as if it's not found in db we 
                //want it to throw an exception.
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.DOB = customer.DOB;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;



            }

            //saving the changes that was made to the local memory.
            //_context.Customers.Add(customer);
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

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            //if the specified customer does not exist we're going to give the user a standard 404 error.
            if (customer == null)
            {
                return HttpNotFound();
            }

            //Creating a NewCustomerViewModel as this is what the New Action takes as an input.
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            //going to the New page which we will use to edit the customers name.
            return View("New", viewModel);
        }


    }

}