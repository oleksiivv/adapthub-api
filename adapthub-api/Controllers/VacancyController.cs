using adapthub_api.Models;
using adapthub_api.Repositories;
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
        private readonly IVacancyRepository _vacancyRepository;

        public VacancyController(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        [HttpGet]
        public IEnumerable<Vacancy> Get([FromBody] FilterVacancyViewModel filter, int from = 0, int to = 10, string sort = "Id")
        {
            return _vacancyRepository.List(filter, sort, from, to);
        }

        [HttpGet("{id}")]
        public Vacancy Get(int id)
        {
            return _vacancyRepository.Find(id);
        }

        [HttpPost]
        public Vacancy Post([FromBody] CreateVacancyViewModel data)
        {
            return _vacancyRepository.Create(data);
        }

        [HttpPut("{id}")]
        public Vacancy Put([FromBody] UpdateVacancyViewModel data)
        {
            return _vacancyRepository.Update(data);
        }

        [HttpDelete("{id}")]
        public Vacancy Delete(int id)
        {
            return _vacancyRepository.Delete(id);
        }
    }
}
