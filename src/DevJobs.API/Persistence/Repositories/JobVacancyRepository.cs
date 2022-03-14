using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence.Repositories
{
    public class JobVacancyRepository : IJobVacancyRepository
    {
        private readonly DevJobsContext _context;

        public JobVacancyRepository(DevJobsContext context)
        {
            _context = context;
        }

        List<JobVacancy> IJobVacancyRepository.GetAll()
        {
            var jobVacancies = _context.JobVacancies
                .Include(x => x.Applications)
                .AsNoTracking()
                .ToList();

            return jobVacancies;
        }

        JobVacancy IJobVacancyRepository.GetById(int id)
        {
            var jobVacancy = _context.JobVacancies
                .Include(jv => jv.Applications)
                .AsNoTracking()
                .SingleOrDefault(jv => jv.Id == id);

            return jobVacancy;
        }

        public void Add(JobVacancy jobVacancy)
        {
            _context.JobVacancies.Add(jobVacancy);
            _context.SaveChanges();
        }

        public void Update(JobVacancy jobVacancy)
        {
            _context.JobVacancies.Update(jobVacancy);
            _context.SaveChanges();
        }

        public void AddApplication(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            _context.SaveChanges();
        }
    }
}
