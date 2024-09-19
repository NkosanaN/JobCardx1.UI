using TimeCard.Domain;

namespace TimeCard.Service.Repository.Interfaces;

public interface IJobCardService
{
    Task<List<JobCard>> GetJobCardsAsync();

    Task<JobCard> CreateJobCardAsync(JobCard jobCard);

    Task<JobCard> GetJobCardAsync(int JobCardId);
}

