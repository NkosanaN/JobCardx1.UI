using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeCard.Domain;
using TimeCard.Service.Repository.Interfaces;
using TimeCard.UI.Helpers;

namespace TimeCard.UI.Controllers;

public class JobCardController : BaseController
{
    private readonly IJobCardService _jobCardService;
    private readonly IJobService _jobService;
    private readonly IEmployeeService _employeeService;

    private readonly ILogger<JobCardController> _logger;
    private IEnumerable<SelectListItem>? JobList { get; set; }
    private IEnumerable<SelectListItem>? EmployeeList { get; set; }

    public JobCardController(
        ILogger<JobCardController> logger, IJobCardService jobCardService, IJobService jobService, IEmployeeService employeeService)
    {
        _logger = logger;
        _jobCardService = jobCardService;
        _jobService = jobService;
        _employeeService = employeeService;
    }

    public async Task<ActionResult> Index()
    {
        var data = await _jobCardService.GetJobCardsAsync();

        _logger.LogInformation("GetJobCardsAsync");

        return View(data);
    }


    public async Task<ActionResult> Create()
    {
        var model = new JobCard
        {
            DateWorked = DateTime.UtcNow,
        };

        var loadJobs = await _jobService.GetJobsAsync();

        var loadEmployees = await _employeeService.GetEmployeeAsync();

        var dataJobs = loadJobs
                        .AsQueryable()
                        .Select(x => new { Value = x.JobId, Text = $"{x.JobId} - ({x.JobType})" })
                        .ToList();

        var dataEmployees = loadEmployees
                    .AsQueryable()
                    .Select(x => new { Value = x.EmployeeId, Text = $"{x.FirstName} - ({x.LastName})" })
                    .ToList();

        JobList = dataJobs.Select(i => new SelectListItem
        {
            Text = i.Text,
            Value = i.Value!.ToString()
        });


        EmployeeList = dataEmployees.Select(i => new SelectListItem
        {
            Text = i.Text,
            Value = i.Value!.ToString()
        });

        ViewBag.JobList = JobList;
        ViewBag.EmployeeList = EmployeeList;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(JobCard jobCard)
    {
        try
        {
            _logger.LogInformation("CreateJobCardAsync");

            if (jobCard.HoursWorked is 0)
            {
                Notify("JobCard", "Hours worked cannot be zero.", type: NotificationType.info);
                return View();
            }

            await _jobCardService.CreateJobCardAsync(jobCard);

            Notify("JobCard", "Successful Add Job Card.", type: NotificationType.success);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Notify("Item", ex.Message.ToString(), type: NotificationType.error);
        }

        var loadJobs = await _jobService.GetJobsAsync();

        var loadEmployees = await _employeeService.GetEmployeeAsync();


        var dataJobs = loadJobs
            .AsQueryable()
            .Select(x => new { Value = x.JobId, Text = $"{x.JobId} - ({x.JobType})" })
            .ToList();

        var dataEmployees = loadEmployees
                    .AsQueryable()
                    .Select(x => new { Value = x.EmployeeId, Text = $"{x.FirstName} - ({x.LastName})" })
                    .ToList();

        JobList = dataJobs.Select(i => new SelectListItem
        {
            Text = i.Text,
            Value = i.Value!.ToString()
        });


        EmployeeList = dataEmployees.Select(i => new SelectListItem
        {
            Text = i.Text,
            Value = i.Value!.ToString()
        });

        ViewBag.JobList = JobList;
        ViewBag.EmployeeList = EmployeeList;

        return View(jobCard);
    }
}
