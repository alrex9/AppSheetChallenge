using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using AppSheet.Models;

namespace AppSheet.Controllers
{
    public class HomeController : Controller
    {
        private static RestClient Client = new RestClient("https://appsheettest1.azurewebsites.net/sample/");

        public IActionResult Index()
        {
            var allIds = this.getAllIds();
            var people = this.getAllValidPeople(allIds);
            var sortedPeople = this.sortPeople(people);            
            return View(sortedPeople);
        }

        private List<int> getAllIds()
        {
            string token = string.Empty;
            var allIds = new List<int>();
            while (token != null)
            {
                var listResult = getNextResult(token);
                allIds.AddRange(listResult.Result);
                token = listResult.Token;
            }

            return allIds;
        }

        private List<Person> getAllValidPeople(List<int> allIds)
        {
            var people = new List<Person>();
            foreach (var id in allIds)
            {
                try
                {
                    var person = this.getPerson(id);
                    if (this.checkPhoneNumber(person))
                    {
                        people.Add(person);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to retrieve person with id {id}");
                }
            }

            return people;
        }

        private IEnumerable<Person> sortPeople(List<Person> people)
        {
            people.Sort();
            var fiveYoungest = people.Take(5);
            fiveYoungest = fiveYoungest.OrderBy(x => x.Name).ToList();
            return fiveYoungest;
        }

        private ListResult getNextResult(string token)
        {
            var request = new RestRequest("list", Method.GET);
            request.AddParameter("token", token);
            var result = Client.Execute(request);
            var listResult = JsonConvert.DeserializeObject<ListResult>(result.Content);
            return listResult;
        }

        private Person getPerson(int id)
        {
            var request = new RestRequest("detail/{id}", Method.GET);
            request.AddUrlSegment("id", id);
            var result = Client.Execute(request);
            var person = JsonConvert.DeserializeObject<Person>(result.Content);
            return person;            
        }

        private bool checkPhoneNumber(Person person)
        {
            if (string.IsNullOrEmpty(person.Number))
            {
                return false;
            }

            var regex = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
            return regex.IsMatch(person.Number);
        }
    }
}
