namespace HR_System.BLL.ModelVM.Attendance
{
    public class GetAttendanceVM
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;

        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public DateTime AttendanceDate { get; set; }

        public string TotalHours
        {
            get
            {
                if (CheckOut.HasValue)
                {
                    var duration = CheckOut.Value - CheckIn;
                    return $"{(int)duration.TotalHours} an hour and {duration.Minutes} minute";
                }
                return "No departure";
            }
        }
    }
}
