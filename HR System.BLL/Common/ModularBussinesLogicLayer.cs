using HR_System.BLL.Mapper;
using HR_System.BLL.Service.Implementation;
using HR_System.DAL.Database;
using HR_System.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace HR_System.BLL.Common
{
    public static class ModularBussinesLogicLayer
    {
        public static IServiceCollection AddBussinesInBLL(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


        

            return services;
        }
    }
}
