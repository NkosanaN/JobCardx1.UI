using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeCard.Domain;
using TimeCard.Service.Repository.Interfaces;
using TimeCard.UI.Helpers;

namespace TimeCard.UI.Controllers;

public class EmployeeController : BaseController
{
    private readonly IEmployeeService _employeeService;

    private readonly ILogger<JobCardController> _logger;

    public EmployeeController(
        ILogger<JobCardController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    public async Task<ActionResult> Index()
    {
        var data = await _employeeService.GetEmployeeAsync();

        return View(data);
    }

    public async Task<ActionResult> Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(JobCard jobCard)
    {
        //try
        //{
        //    if (jobCard.HoursWorked is 0)
        //    {
        //        Notify("JobCard", "Hours worked cannot be zero.", type: NotificationType.info);
        //        return View();
        //    }

        //    await _employeeService.CreateJobCardAsync(jobCard);

        //    Notify("JobCard", "Successful Add Job Card.", type: NotificationType.success);

        //    return RedirectToAction(nameof(Index));
        //}
        //catch (Exception ex)
        //{
        //    Notify("Item", ex.Message.ToString(), type: NotificationType.error);
        //}

        return View(jobCard);
    }
}
