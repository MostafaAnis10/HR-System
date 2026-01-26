using System;
using System.Collections.Generic;
using System.Text;

namespace HR_System.BLL.ModelVM.Attendance
{
    public class DetailsAttendanceVM
    {
        public int Id { get; set; }

        // Employee Info
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        // Attendance Info
        public DateTime AttendanceDate { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        // Computed Property
        public string TotalHours
        {
            get
            {
                if (!CheckOut.HasValue)
                    return "—";

                var total = CheckOut.Value - CheckIn;
                return $"{(int)total.TotalHours:D2}:{total.Minutes:D2}";
            }
        }

        // Helpers for UI
        public bool IsCheckedOut => CheckOut.HasValue;
    }
}
