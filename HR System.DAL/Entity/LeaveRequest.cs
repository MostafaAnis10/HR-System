namespace HR_System.DAL.Entity
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public LeaveType LeaveType { get; set; } 

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    }
}
