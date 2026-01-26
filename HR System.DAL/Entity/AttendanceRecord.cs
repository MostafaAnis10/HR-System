using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.DAL.Entity
{
    public class AttendanceRecord
    {
        protected AttendanceRecord() { }
        public AttendanceRecord(DateTime checkIn, DateTime? checkOut, DateTime attendanceDate, int employeeId )
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            AttendanceDate = attendanceDate;
            this.EmployeeId = employeeId;
        }
        public int Id { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public DateTime AttendanceDate { get; set; }
        
        public void RegisterCheckOut()
        {
            if (this.CheckOut == null)
            {
                this.CheckOut = DateTime.Now;
            }
        }

    }
}
