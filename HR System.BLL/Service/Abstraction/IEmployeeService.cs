
using HR_System.BLL.ModelVM.Employee;

namespace HR_System.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Response<List<GetEmployeeVM>> GetActiveEmployee();
        Response<List<GetEmployeeVM>> GetNotActiveEmployee();
        Response<CreateEmployeeVM> Create(CreateEmployeeVM model, string? fileName);
        Response<EditEmployeeVM> Edit(EditEmployeeVM model);
        Response<EmployeeDetailsVM> GetDetails(int id);
        Response<EditEmployeeVM> GetByID(int id); 
        Response<bool> Delete(int id);



    }
}
