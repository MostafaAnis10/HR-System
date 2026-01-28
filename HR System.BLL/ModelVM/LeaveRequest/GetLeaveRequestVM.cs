
namespace HR_System.BLL.ModelVM.LeaveRequest
{
    public class GetLeaveRequestVM
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; } = null!;

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public LeaveType LeaveType { get; set; }
        public LeaveStatus Status { get; set; }

        public int TotalDays =>
            (ToDate - FromDate).Days + 1;
    }
}
