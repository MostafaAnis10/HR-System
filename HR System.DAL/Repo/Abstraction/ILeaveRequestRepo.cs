using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Abstraction
{
    public interface ILeaveRequestRepo
    {
        //Command
        bool Add(LeaveRequest leaveRequest);
        bool Edit(LeaveRequest leaveRequest);
        bool Delete(int id);

        //Quary
        LeaveRequest GetById(int id);
        List<LeaveRequest> GetAll(Expression<Func<LeaveRequest, bool>>? Filter = null);
    }
}
