using System;
using System.Collections.Generic;
using System.Text;

namespace HR_System.BLL.ModelVM.Employee
{
    public class EmployeeDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
