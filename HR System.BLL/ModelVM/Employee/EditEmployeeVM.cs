using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_System.BLL.ModelVM.Employee
{
    public class EditEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }

        // أضف هذه الحقول للتعامل مع الصورة في الـ UI والـ Controller
        public IFormFile? NewFile { get; set; }
        public string? File { get; set; } // هذا سيحمل اسم الصورة القديمة من الداتابيز

    }
}
