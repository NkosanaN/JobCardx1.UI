using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeCard.Domain;
using TimeCard.Service.Repository.Interfaces;
using TimeCard.UI.Helpers;

namespace TimeCard.UI.Controllers
{
    public class JobController : BaseController
    {
        public readonly IJobService _jobService;
        private readonly ILogger<JobController> _logger;
        private IEnumerable<SelectListItem>? JobTypeList { get; set; }

        public JobController(ILogger<JobController> logger, IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _jobService.GetJobsAsync();

            _logger.LogInformation("GetJobsAsync");

            return View(data);
        }

        public async Task<ActionResult> Details(string jobId)
        {
            var data = await _jobService.GetJobAsync(jobId);

            _logger.LogInformation("GetJobAsync");

            return View(data);
        }

        public async Task<ActionResult> Create()
        {
            var model = new Job { DateCreated = DateTime.UtcNow };

            var JobTypes = new Dictionary<int, string>
            {
                { 1, "Repair" },
                { 2, "Support" },
                { 3, "Warranty" }
            };

            var dataJobTypes = JobTypes
                .Select(x => new { Value = x.Key, Text = x.Value })
                .ToList();

            JobTypeList = dataJobTypes.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            ViewBag.jobTypeList = JobTypeList;

            await Task.Delay(0);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Job job)
        {
            try
            {
                await _jobService.CreateJobAsync(job);

                Notify("Job", "Successful Add job.", type: NotificationType.success);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Notify("Job", ex.Message.ToString(), type: NotificationType.error);
            }

            return View(job);
        }
    }
}
