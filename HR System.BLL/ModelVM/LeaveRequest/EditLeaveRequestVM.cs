using System.ComponentModel.DataAnnotations;
namespace HR_System.BLL.ModelVM.LeaveRequest
{
    public class EditLeaveRequestVM
    {
        [Required]
        public int Id { get; set; }

        // للعرض فقط (Label)
        public string EmployeeName { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

        // للعرض فقط
        public LeaveStatus Status { get; set; }
    }
}
