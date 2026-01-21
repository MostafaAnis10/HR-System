using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR_System.DAL.Entity
{
    
    public class Department
    {
        protected Department() {  }

        public Department( string name , string creatorUser)
        {
            
            Name = name;
            CreateBy = creatorUser;
        }

        public int Id { get;private set; }
        public string Name { get;private set; }

        public List<Employee> Employees { get; set; }

        public DateTime CreateOn { get; private set; } = DateTime.Now;
        public DateTime? updateOn { get; private set; }
        public DateTime? DeleteOn { get; private set; }
        public string? CreateBy { get; private set; }
        public string? updateBy { get; private set; }
        public string? DeleteBy { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public bool Update(string name, string UserModified)
        {
            if (!string.IsNullOrEmpty(UserModified))
            {
                Name = name;
                updateOn = DateTime.Now;
                updateBy = UserModified;

                return true;
            }

            return false;
        }

        public bool ToggaleStatus(string DeletedUser)
        {
            if (!string.IsNullOrEmpty(DeletedUser))
            {
                IsDeleted = !IsDeleted;
                DeleteBy = DeletedUser;
                DeleteOn = DateTime.Now;

                return true;
            }

            return false;
        }
    }
}
