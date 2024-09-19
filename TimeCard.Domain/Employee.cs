using System.ComponentModel.DataAnnotations;

namespace TimeCard.Domain;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }
    public string Email { get; set; }
}
