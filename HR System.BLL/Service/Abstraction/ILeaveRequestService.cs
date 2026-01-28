
using HR_System.BLL.ModelVM.LeaveRequest;

namespace HR_System.BLL.Service.Abstraction
{
    public interface ILeaveRequestService
    {
        Response<List<GetLeaveRequestVM>> GetAll();
        Response<DetailsLeaveRequestVM> GetById(int id);
        Response<bool> Create(CreateLeaveRequestVM model);
        Response<bool> Update(int id, CreateLeaveRequestVM model);
        Response<bool> Delete(int id);
        Response<bool> ChangeStatus(int id, string status);
    }
}
