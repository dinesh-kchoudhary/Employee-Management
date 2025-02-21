using Bogus;
using Sharp.Models;

namespace Sharp.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            if (context.Employees.Any()) return; // Skip if data already exists

            var faker = new Faker();
            var jobTitles = new List<string>
        {
            "Software Engineer", "Project Manager", "HR Specialist", "Data Analyst",
            "System Administrator", "Marketing Manager", "Accountant", "UI/UX Designer",
            "IT Support", "DevOps Engineer", "Business Analyst", "Sales Representative"
        };

            for (int i = 0; i < 100; i++)
            {
                var employee = new Employee
                {
                    Name = faker.Name.FullName(),
                    SSN = $"{faker.Random.Number(100, 999)}-{faker.Random.Number(10, 99)}-{faker.Random.Number(1000, 9999)}",
                    DOB = faker.Date.Past(30, DateTime.Now.AddYears(-22)),
                    Address = faker.Address.FullAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    Zip = faker.Address.ZipCode(),
                    Phone = faker.Phone.PhoneNumber(),
                    JoinDate = faker.Date.Past(5),
                    ExitDate = faker.Date.Future()
                };

                context.Employees.Add(employee);
                await context.SaveChangesAsync(); // Save to get Employee ID

                var salary = new EmployeeSalary
                {
                    EmployeeId = employee.Id, // Assign correct Employee ID
                    Title = faker.PickRandom(jobTitles),
                    Salary = faker.Finance.Amount(30000, 150000),
                    FromDate = employee.JoinDate,
                    ToDate = employee.ExitDate ?? DateTime.Now
                };

                context.EmployeeSalaries.Add(salary);
            }

            await context.SaveChangesAsync();
        }
    }
}
