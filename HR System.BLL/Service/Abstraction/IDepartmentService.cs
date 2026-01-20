using HR_System.BLL.ModelVM.Department;

namespace HR_System.BLL.Service.Abstraction
{
    public interface IDepartmentService
    {
        Response<List<GetDepartmentVM>> GetDepartment();
        Response<EditDepartementVM> EditDepartment(EditDepartementVM model);
        Response<CreateDepartementVM> CreateDepartment(CreateDepartementVM model);
        Response<EditDepartementVM> GetByID(int id);
        Response<bool> Delete(int id);
    }
}
