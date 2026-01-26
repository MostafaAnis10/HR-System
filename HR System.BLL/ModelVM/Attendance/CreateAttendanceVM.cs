
using System.ComponentModel.DataAnnotations;

namespace HR_System.BLL.ModelVM.Attendance
{
    public class CreateAttendanceVM
    {
        [Required]
        public int EmployeeId { get; set; }

    }
}
