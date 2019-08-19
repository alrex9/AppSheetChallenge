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
        [JsonProperty("result")]
        public IEnumerable<int> Result { get; private set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; private set; }
    }
}
