using AutoMapper;
using HR_System.BLL.ModelVM.Department;
using HR_System.DAL.Entity;

namespace HR_System.BLL.Mapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            CreateMap<Employee ,GetEmployeeVM> ().ReverseMap();
            CreateMap<CreateEmployeeVM, Employee>();
            CreateMap<Department ,GetDepartmentVM> ().ReverseMap();
            CreateMap<Department, EditDepartementVM>().ReverseMap();
            CreateMap<Employee, EditEmployeeVM>().ReverseMap();
            CreateMap<Employee, EmployeeDetailsVM>()
                .ForMember(
        dest => dest.DepartmentName,
        opt => opt.MapFrom(src => src.Department.Name)
    );
        }
    }
}
