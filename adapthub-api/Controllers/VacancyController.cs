using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyRepository vacancyRepository;

        public VacancyController(IVacancyRepository vacancyRepository)
        {
            this.vacancyRepository = vacancyRepository;
        }

        // GET: api/<VacancyController>
        [HttpGet]
        public IEnumerable<string> Get([FromBody] FilterVacancyViewModel data, int limit = 5, int offset = 0, string sort = "Id")
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] CreateVacancyViewModel data)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateVacancyViewModel data)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
