using System.ComponentModel.DataAnnotations;

namespace HR_System.BLL.ModelVM.LeaveRequest
{
    public class CreateLeaveRequestVM
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }
    }
}
