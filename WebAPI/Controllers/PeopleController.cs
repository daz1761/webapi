using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This is where I give you all the information about this API
    /// </summary>

    public class PeopleController : ApiController
    {
        List<Person> people = new List<Person>();

        // contructor - don't make calls directly to a DB at the API level, these should be in a separate class library (business logic)
        public PeopleController()
        {
            people.Add(new Person { FirstName = "Tim", LastName = "Corey", Id = 1 });
            people.Add(new Person { FirstName = "Sue", LastName = "Price", Id = 2 });
            people.Add(new Person { FirstName = "Dave", LastName = "Mustain", Id = 3 });
        }

        /// <summary>
        /// Gives us a list of the first name of each user
        /// </summary>
        /// <param name="userId">The ID for this person</param>
        /// <param name="age">We want to know how old they are</param>
        /// <returns>Obviously, a list of names, Doh!!!!</returns>
        // we want a separate GET call in our API
        // we have to override the default route
        // we can have arguments (none optional)
        [Route("api/People/GetFirstNames/{userId:int}/{age:int}")]
        [HttpGet]
        public List<string> GetFirstNames(int userId, int age)
        {
            List<string> output = new List<string>();

            foreach(var p in people)
            {
                output.Add(p.FirstName);
            }
            return output;
        }

        // GET: api/People
        // we can also add a query string: http://webapi.local/api/People?firstName=Mary&lastName=Smith
        // (1) but if we have & in the value, we would have to escape it out
        // (2) a url restricted is to 2000 characters by http (e.g. a big form)
        // (3) sensitive data will be exposed (e.g. a person creating an account) even with HTTPS
        public List<Person> Get()
        {
            return people;
        }

        // GET: api/People/5
        public Person Get(int id)
        {
            return people.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/People
        // POST uses the BODY which is unlimited in terms of characters
        // we could post information then use it to filter and return a particular Person
        public void Post(Person person)
        {
            // this would normally add to a DB, but what happens is that the instance above gets wiped and will only show the new Person
            people.Add(person);
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
            // similarly we could have this to delete an entry from a DB via Id
            people.Where(x => x.Id == id);
        }
    }
}
