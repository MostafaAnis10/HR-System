

namespace HR_System.DAL.Entity
{
    public class Position
    {
        public Position(string title, decimal defaultSalary)
        {
            Title = title;
            DefaultSalary = defaultSalary;
        }

        public int Id { get;  set; }
        public string Title { get;  set; } = null!;
        public decimal DefaultSalary { get;  set; }

        public List<Employee> Employees { get; set; }

    }
}
