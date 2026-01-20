using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace HR_System.DAL.Entity
{
    public class ApplicationUser : IdentityUser
    {
        
        public string? EmployeeId { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
