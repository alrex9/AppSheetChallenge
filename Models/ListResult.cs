using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppSheet.Models
{
    /// <summary>
    /// Data continer holding the list of people retrieved in the current query and a token to the start of the next list.
    /// </summary>
    public class ListResult
    {
        /// <summary>
        /// Create the list of people and find the next token based on the given set of people.
        /// </summary>
        public ListResult(IEnumerable<Person> people)
        {           
            this.Result = people.Select(x => x.Id);
            if (people.Count() == 10)
            {
                var lastItem = people.Last();
                var token = lastItem.Hash.ToString();
                this.Token = token;
            }
        }

        [JsonProperty("result")]
        public IEnumerable<int> Result { get; private set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; private set; }
    }
}
