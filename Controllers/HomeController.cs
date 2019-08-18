using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppSheet.Models;

namespace AppSheet.Controllers
{
    public class HomeController : Controller
    {
        private PersonController personController;

        public HomeController()
        {
            this.personController = new PersonController();
        }

        /// <summary>
        /// Get the model for the homepage.
        /// </summary>
        public IActionResult Index()
        {
            // Instead of calling the controller you could call the endpoint but this was easier.
            var listResult = this.personController.ListPeople(string.Empty);
            var ids = listResult.Result.ToList();
            var people = new List<Person>();
            for (var index = 0; index < 5; index++)
            {
                var id = ids[index];
                var person = this.personController.GetDetails(id);
                people.Add(person);
            }

            people = people.OrderBy(x => x.Name).ToList();
            return View(people);
        }
    }
}
