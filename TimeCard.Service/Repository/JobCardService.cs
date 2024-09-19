using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using TimeCard.Domain;
using TimeCard.Service.Repository.Interfaces;

namespace TimeCard.Service.Repository;

public class JobCardService : IJobCardService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    public JobCardService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }

    public async Task<List<JobCard>> GetJobCardsAsync()
    {
        var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/JobCard");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot retrieve tasks");
        }

        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<List<JobCard>>(content);

        return response!;
    }

    public Task<JobCard> GetJobCardAsync(int JobCardId)
    {
        throw new NotImplementedException();
    }

    public async Task<JobCard> CreateJobCardAsync(JobCard jobCard)
    {
        try
        {
            
            var content = JsonConvert.SerializeObject(jobCard);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl").Value}/JobCard",
                new StringContent(content, Encoding.Default, "application/json"));

            if (httpResponse.ReasonPhrase!.Contains("Bad Request"))
            {
                throw new Exception("job already exist");
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the JobCard");
            }

            var response =
                JsonConvert.DeserializeObject<JobCard>(await httpResponse.Content.ReadAsStringAsync());

            return response!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
