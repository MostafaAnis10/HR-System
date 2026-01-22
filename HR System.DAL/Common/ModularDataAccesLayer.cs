using HR_System.DAL.Repo.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_System.DAL.Common
{
    public static class ModularDataAccesLayer 
    {
        public static IServiceCollection AddBussinesInDAL(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IPositionRepo, PositionRepo>();
            return services;
        }
    }
}
