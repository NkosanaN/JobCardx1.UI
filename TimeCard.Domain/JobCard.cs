using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TimeCard.Domain;
public class JobCard
{
    [DisplayName("Job Card")]
    public int JobCardId { get; set; }

    [DisplayName("Employee")]
    public int EmployeeId { get; set; }

    [DisplayName("Job")]
    public string JobId { get; set; }
    public Job Job { get; set; }

    public Employee Employee { get; set; }

    [DisplayName("Date Worked")]
    [DataType(DataType.Date)]
    public DateTime DateWorked { get; set; }

    [DisplayName("Hours Worked")]
    [Range(0.5, 24, ErrorMessage = "Hours worked must be between 0.5 and 24 hours.")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid hours format.")]
    public float HoursWorked { get; set; }

    //public override string Emp => Employee.FirstName + " " + Employee.LastName;
}
