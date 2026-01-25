using HR_System.BLL.ModelVM.Attendance;

namespace HR_System.BLL.Service.Abstraction
{
    public interface IAttendanceService
    {
        Response<List<GetAttendanceVM>> GetAll();
        Response<EditAttendanceVM> GetByID(int id);
        Response<CreateAttendanceVM> Create(CreateAttendanceVM model);
        Response<EditAttendanceVM> Edit(EditAttendanceVM model);
        Response<bool> Delete(int id);
    }
}
