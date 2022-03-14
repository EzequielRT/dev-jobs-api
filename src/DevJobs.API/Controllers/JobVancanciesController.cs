using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVancanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;

        public JobVancanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy is null)
                return NotFound();

            return Ok(jobVacancy);
        }

        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>
        /// <remarks>
        ///   {
        ///   "title": "Vaga .NET Jr",
        ///   "description": "Vaga para uma grande empresa.",
        ///   "company": "LuisDev",
        ///   "isRemote": true,
        ///   "salaryRange": "3000-5000"
        ///   }
        /// </remarks>
        /// <param name="model">Dados de Vaga</param>
        /// <returns>Objeto recém criado.</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("POST JobVacancy chamado");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _repository.Add(jobVacancy);

            return CreatedAtAction("GetById", new { id = jobVacancy.Id }, jobVacancy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy is null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            _repository.Update(jobVacancy);

            return NoContent();
        }
    }
}
