
using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Abstraction
{
    public interface IAttendanceRepo
    {
        //Command
        bool Add(AttendanceRecord attendance);
        bool Edit(AttendanceRecord attendance);
        bool Delete(int id);

        //Quary
        AttendanceRecord GetById(int id);
        List<AttendanceRecord> GetAll(Expression<Func<AttendanceRecord, bool>>? Filter = null);
    }
}
