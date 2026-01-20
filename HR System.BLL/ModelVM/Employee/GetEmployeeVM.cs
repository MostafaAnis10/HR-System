
namespace HR_System.BLL.ModelVM.Employee
{
    public class GetEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string? File { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;



    }
}
