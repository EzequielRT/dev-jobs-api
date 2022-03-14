namespace DevJobs.API.Entities
{
    public class JobApplication
    {
        public JobApplication(string applicationName, string applicationEmail, int idJobVacancy)
        {
            ApplicationName = applicationName;
            ApplicationEmail = applicationEmail;
            IdJobVacancy = idJobVacancy;
        }

        public int Id { get; private set; }
        public string ApplicationName { get; private set; }
        public string ApplicationEmail { get; private set; }
        public int IdJobVacancy { get; private set; }
    }
}
