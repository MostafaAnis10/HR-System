using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.DAL.Entity
{
    public class Employee
    {
        protected Employee() { }
        public Employee(string name ,decimal salary, int? DepartmentId,string? file, string creatorUser)
        {
            Name = name;
            Salary = salary;
            CreateBy = creatorUser;
            File = file;
            CreateOn = DateTime.Now;
            this.DepartmentId = DepartmentId;
        }

        public int Id { get;private set; }
        public string Name { get; private set; }
        public decimal Salary { get; private set; }
        public string? File { get; private set; }
        public DateTime CreateOn { get; private set; } = DateTime.Now;
        public DateTime? updateOn { get; private set; }
        public DateTime? DeleteOn { get; private set; }
        public string? CreateBy { get; private set; }
        public string? updateBy { get; private set; }
        public string? DeleteBy { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; private set; }
        public Department? Department { get; private set; }

        [ForeignKey(nameof(Position))]
        public int? PositionId { get; private set; }
        public Position? Position { get; private set; }
        public List<AttendanceRecord> attendanceRecords { get; private set; }
        public bool Update(string name , decimal salary,int? departmentId , string UserModified)
        {
            if(!string.IsNullOrEmpty(UserModified) )
            {
                Name = name;
                Salary = salary;
                DepartmentId = departmentId;
                updateOn = DateTime.Now;
                updateBy = UserModified;

                return true;
            }

            return false;
        }

        public bool ToggaleStatus(string DeletedUser)
        {
            if (!string.IsNullOrEmpty(DeletedUser))
            {
                IsDeleted = !IsDeleted;
                DeleteBy = DeletedUser;

                return true;
            }

            return false;
        }

        public void SetFile(string fileName)
        {
            File = fileName;
        }
    }
}
