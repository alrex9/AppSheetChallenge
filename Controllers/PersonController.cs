using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppSheet.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppSheet.Controllers
{
    /// <summary>
    /// Data controller used for accessing readonly methods of People data.
    /// </summary>
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private const string FileName = "PeopleData.json";

        public const int PeoplePerList = 10;

        private List<Person> people;
        
        /// <summary>
        /// Instantiate the cotroller by loading the people data into memory.
        /// </summary>
        public PersonController()
        {
            this.loadPeople();
        }

        /// <summary>
        /// Retrieves the first 10 people if no token is given.
        /// If a token is given then the next 10 people appaearing after the token are retrieved.
        /// </summary>
        [HttpGet("list")]
        public ListResult ListPeople(string token)
        {
            var indexOfToken = this.people.FindIndex(x => x.Hash.ToString() == token);
            var firstPeople = this.people.Skip(indexOfToken + 1).Take(PeoplePerList);
            var listResult = new ListResult(firstPeople);
            return listResult;
        }

        /// <summary>
        /// Retruns the details of the person with the corresponding id.
        /// </summary>
        [HttpGet("detail/{id}")]
        public Person GetDetails(int id)
        {
            return this.people.Single(x => x.Id == id);
        }

        /// <summary>
        /// Load people data from a text file.
        /// </summary>
        private void loadPeople()
        {
            string path = this.getFileName();
            var fileContents = System.IO.File.ReadAllText(path);
            this.people = JsonConvert.DeserializeObject<List<Person>>(fileContents);
            this.people.Sort();
        }

        /// <summary>
        /// Helper for reacting the path to the people data file.
        /// </summary>
        /// <returns></returns>
        private string getFileName()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);
        }
    }
}
