// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace DemoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //Listen for events of created, update, deleted
        // Mock data store for simplicity
        private static List<Person> names = new List<Person>
        {
            new Person { Id = 1, Name = "John Doe (Sample)", Email = "john@doe.com", Created = DateTime.Now },
        };

        // GET: api/allpeople
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAllPeople()
        {
            //Get all people from the database
            return Ok(names);
        }

        // GET: api/person/1
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            //Get Person form the database with specific id
            var person = names.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // POST: api/people
        [HttpPost]
        public ActionResult<Person> CreatePerson(Person name)
        {
            var corId = Guid.NewGuid(); //Create a correlation id for callback
            name.Id = names.Max(p => p.Id) + 1; //If id is 0, create an id
            names.Add(name);
            return CreatedAtAction(nameof(GetPersonById), new { id = name.Id }, name);

            //Publish Create Message (Person)
        }

        // PUT: api/person/1
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(int id, Person updatedName)
        {
            var corId = Guid.NewGuid(); //Create a correlation id for callback
            var person = names.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            person.Name = updatedName.Name;

            //Publish Update Message (Person)

            return NoContent();
        }

        // DELETE: api/person/1
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int id)
        {
            var corId = Guid.NewGuid(); //Create a correlation id for callback
            var name = names.FirstOrDefault(p => p.Id == id);

            if (name == null)
            {
                return NotFound();
            }

            //Publish delete message (id)

            names.Remove(name);
            return NoContent();
        }
    }
}
