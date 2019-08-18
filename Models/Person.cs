using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSheet.Models
{
    /// <summary>
    /// Data contiainer representative of a Person object.
    /// </summary>
    [Serializable]
    public class Person : IComparable
    {
        public static string PeopleEndpoint = "http://localhost:64376/api/Person/";

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public short Age { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }


        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonIgnore]
        public int Hash => this.GetHashCode();

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() * 17;
        }

        public int CompareTo(object obj)
        {
            var otherPerson = obj as Person;
            if (obj != null)
            {
                return otherPerson.Age.CompareTo(this.Age);
            }
            else
            {
                throw new ArgumentException("Object is not of type Person");
            }

        }
    }
}
