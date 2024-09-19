using Microsoft.Extensions.Configuration;
using TimeCard.Domain;
using Newtonsoft.Json;
using System.Text;
using TimeCard.Service.Repository.Interfaces;

namespace TimeCard.Service.Repository;

public class JobService : IJobService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public JobService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }

    public async Task<List<Job>> GetJobsAsync()
    {
        var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/Job");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot retrieve tasks");
        }

        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<List<Job>>(content);

        return response!;
    }

    public async Task<List<Job>> GetJobAsync(string JobId)
    {
        try
        {
            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/Job/{JobId}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve job");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<List<Job>>(content);

            return response!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Job> CreateJobAsync(Job job)
    {
        try
        {
            var content = JsonConvert.SerializeObject(job);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl").Value}/Job",
                new StringContent(content, Encoding.Default, "application/json"));

            if (httpResponse.ReasonPhrase!.Contains("Bad Request"))
            {
                throw new Exception("job already exist");
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the job");
            }

            var response =
                JsonConvert.DeserializeObject<Job>(await httpResponse.Content.ReadAsStringAsync());

            return response!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

  
}
