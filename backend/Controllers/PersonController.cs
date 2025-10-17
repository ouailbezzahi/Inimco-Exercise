using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("analyze")]
        public IActionResult AnalyzePerson([FromBody] Person person)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(person.FirstName) ||
                string.IsNullOrWhiteSpace(person.LastName))
            {
                return BadRequest("First name and last name are required.");
            }

            var analysis = _personService.AnalyzePerson(person);
            _personService.SavePerson(person);

            return Ok(analysis);
        }
    }
}
