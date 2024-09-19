using TimeCard.Domain;

namespace TimeCard.Service.Repository.Interfaces;

public interface IJobService
{
    Task<List<Job>> GetJobsAsync();

    Task<List<Job>> GetJobAsync(string JobId);

    Task<Job> CreateJobAsync(Job job);


}
