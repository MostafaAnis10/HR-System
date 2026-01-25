
using System.ComponentModel.DataAnnotations;

namespace HR_System.BLL.ModelVM.Attendance
{
    public class CreateAttendanceVM
    {
        [Required(ErrorMessage = "should be select Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; } = DateTime.Now;
        [DataType(DataType.Time)]
        public DateTime CheckOut { get; set; } = DateTime.Now;

        [Required]
        public DateTime AttendanceDate { get; set; } = DateTime.Today;
    }
}
