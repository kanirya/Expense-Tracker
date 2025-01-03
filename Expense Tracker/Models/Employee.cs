using System.ComponentModel;

namespace Expense_Tracker.Models
{
    public class Employee
    {

        public Guid Id { get; set; }
        [DisplayName("Employee Name")]
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }




    public class AddEmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public  string? Phone { get; set; }
        public decimal Salary { get; set; }
    }

    
          public class UpdateEmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }

}
