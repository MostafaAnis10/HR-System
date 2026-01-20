
using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly HrDbContext HrDb;

        public EmployeeRepo(HrDbContext hrDb)
        {
            HrDb = hrDb;
        }
        public bool Add(Employee employee)
        {
            try
            {
                var result = HrDb.employees.Add(employee);
                HrDb.SaveChanges();
                if (result.Entity.Id > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Edit(Employee newEmployee)
        {
            try
            {
                var oldEmployee = HrDb.employees.Where(a => a.Id == newEmployee.Id).FirstOrDefault();
                if (oldEmployee != null)
                {
                    var result = oldEmployee.Update(newEmployee.Name, newEmployee.Salary,newEmployee.DepartmentId, "Mostafa");
                    if (result)
                    {
                        HrDb.SaveChanges();
                        return true;
                    }

                    return false;

                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Employee> GetActiveEmployee()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll(Expression<Func<Employee, bool>>? Filter = null)
        {
            try
            {
                if (Filter != null)
                {

                    var result = HrDb.employees.Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = HrDb.employees.ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Employee GetById(int id)
        {
            try
            {
                var result = HrDb.employees.Include(e=>e.Department ).FirstOrDefault(e => e.Id == id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ToggaleStatus(int id)
        {
            try
            {
                var oldemployee = HrDb.employees.Where(e => e.Id == id).FirstOrDefault();
                if (oldemployee != null)
                {

                    var result = oldemployee.ToggaleStatus("Anis");
                    if (result)
                    {
                        HrDb.SaveChanges();
                        return true;
                    }
                    return false;

                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

