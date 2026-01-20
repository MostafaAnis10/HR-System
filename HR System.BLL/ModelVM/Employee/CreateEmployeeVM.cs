
using HR_System.BLL.Helper;
using Microsoft.AspNetCore.Http;

namespace HR_System.BLL.ModelVM.Employee
{
    public class CreateEmployeeVM
    {
        public string Name { get;  set; }
        public decimal Salary { get;  set; }
        public int DepartmentId { get; set; }

        public IFormFile? File { get; set; }

    }
}
