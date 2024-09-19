using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TimeCard.Domain;

public class Job
{
    [DisplayName("Job Id")]
    public string JobId { get; set; } = string.Empty;

    [DisplayName("Job Type")]
    public string JobType { get; set; } = string.Empty;

    [DisplayName("Date")]
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; }

    [DisplayName("Client Name")]
    public string ClientName { get; set; } = string.Empty;

    [DisplayName("Client Phone")]
    public string ClientPhone { get; set; } = string.Empty;


    [DisplayName("Client Contact Name")]
    [DataType(DataType.PhoneNumber)]
    public string ClientContactName { get; set; } = string.Empty;
   public List<JobCard> JobCards { get; set; }
}