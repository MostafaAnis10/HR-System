using System.ComponentModel.DataAnnotations;

namespace HR_System.BLL.ModelVM.Attendance
{
    public class EditAttendanceVM
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        // لإظهار الاسم في صفحة التعديل كـ Label فقط
        public string? EmployeeName { get; set; }
               
        public DateTime? CheckOut { get; set; }

        
    }
}
