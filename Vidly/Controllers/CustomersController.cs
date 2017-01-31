using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        private CustomersViewModel customers;

        public CustomersController()
        {
            _context = new ApplicationDbContext();

            customers = new CustomersViewModel
            {
                Customers = _context.Customers.Include(c => c.MembershipType).ToList()
            };
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        



        // GET: Customers
        public ActionResult Index()
        {


           
                

            return View(customers);
           
        }

        public ActionResult Details(int Id)
        {
            

            var customer = new Customer();
            foreach (var customer_var in customers.Customers)
            {
                if (customer_var.Id == Id)
                {
                    customer = customer_var;
                    return View(customer);
                }
                
            }

            return HttpNotFound();
            
        }

        //Ta akcja prawdopodobnie odsyla do formularza tworzenia nowego uzytkownika!
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                //przeslanie membershipTypes sluzy zapelnieniu DropDownList
                MembershipTypes = membershipTypes,
                //przeslanie zainicjowanego customera sluzy wypelnieniu ukrytego inputa liczbą
                Customer = new Customer()
            };

            return View("CustomerForm",viewModel);
        }


        //Ta akcja zapisuje uzytkownika do bazy danych
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };


                return View("CustomerForm", viewModel);
            }
                


            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);
                //Nie korzystaj z tego bo to tworzy dziury w bezpieczeństwie
                //TryUpdateModel(customerInDB);
                customerInDB.Name = customer.Name;
                customerInDB.BirthDate = customer.BirthDate;
                customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;



            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        //Akcja sluzy do edytowania istniejacego uzytkownika, prowadzi do niej 
        //klikniecie w uzytkownia ze strony Customers/index
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);

        }
    }
}