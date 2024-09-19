using Microsoft.Extensions.Configuration;
using TimeCard.Domain;
using Newtonsoft.Json;
using System.Text;
using TimeCard.Service.Repository.Interfaces;

namespace TimeCard.Service.Repository;

public class EmployeeService : IEmployeeService
{ 
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public EmployeeService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }

    public async Task<List<Employee>> GetEmployeeAsync()
    {
        var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/Employee");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot retrieve Employee");
        }

        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<List<Employee>>(content);

        return response!;
    }

    public async Task<Employee> GetEmployeeAsync(string empId)
    {
        try
        {
            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/Employee/{empId}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve Employee");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<Employee>(content);

            return response!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        try
        {
            var content = JsonConvert.SerializeObject(employee);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl").Value}/Employee",
                new StringContent(content, Encoding.Default, "application/json"));

            if (httpResponse.ReasonPhrase!.Contains("Bad Request"))
            {
                throw new Exception("Employee already exist");
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve Employee");
            }

            var response =
                JsonConvert.DeserializeObject<Employee>(await httpResponse.Content.ReadAsStringAsync());

            return response!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
}
