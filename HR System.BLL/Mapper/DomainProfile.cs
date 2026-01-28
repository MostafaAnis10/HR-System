using AutoMapper;
using HR_System.BLL.ModelVM.Attendance;
using HR_System.BLL.ModelVM.Department;
using HR_System.BLL.ModelVM.LeaveRequest;
using HR_System.BLL.ModelVM.Position;
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

            CreateMap<Position, CreatePositionVM>().ReverseMap();
            CreateMap<Position, EditPositionVM>().ReverseMap();
            CreateMap<Position, GetPositionVM>().ReverseMap();


            CreateMap<AttendanceRecord, GetAttendanceVM>()
            .ForMember(dest => dest.EmployeeName, 
            opt => opt.MapFrom(src => src.Employee.Name)); // مهم جداً لعرض الاسم

            CreateMap<EditAttendanceVM, AttendanceRecord>().ReverseMap();
            CreateMap<CreateAttendanceVM, AttendanceRecord>().ReverseMap();
            CreateMap<AttendanceRecord, DetailsAttendanceVM>()
                .ForMember(d => d.EmployeeName,
        opt => opt.MapFrom(s => s.Employee.Name));


            // ===================== Entity → Get =====================
            CreateMap<LeaveRequest, GetLeaveRequestVM>()
                .ForMember(dest => dest.EmployeeName,
               opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.LeaveType,
                           opt => opt.MapFrom(src => src.LeaveType.ToString()))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status.ToString()));

            // ===================== Entity → Details =====================
            CreateMap<LeaveRequest, DetailsLeaveRequestVM>()
    .ForMember(dest => dest.EmployeeName,
               opt => opt.MapFrom(src => src.Employee.Name));
            // ===================== Create VM → Entity =====================
            CreateMap<CreateLeaveRequestVM, LeaveRequest>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => LeaveStatus.Pending));

            // ===================== Edit VM → Entity =====================
            CreateMap<EditLeaveRequestVM, LeaveRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());
        }
    }
}
