namespace Sharp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public ICollection<EmployeeSalary> Salaries { get; set; } = new List<EmployeeSalary>();
    }
}
