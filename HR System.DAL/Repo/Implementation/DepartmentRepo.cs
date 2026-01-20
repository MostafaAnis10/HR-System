
using HR_System.DAL.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Implementation
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly HrDbContext HrDb;

        public DepartmentRepo(HrDbContext HrDb) 
        {
            this.HrDb = HrDb;
        }

        public bool Add(Department department)
        {
            try
            {
                var result = HrDb.Department.Add(department);
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

        public bool Edit(Department newDepartment)
        {
            try
            {
                var oldDempt = HrDb.Department.Where(a => a.Id == newDepartment.Id).FirstOrDefault();
                if (oldDempt != null)
                {
                    var result = oldDempt.Update(newDepartment.Name,  "Mostafa");
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

       

        public List<Department> GetAll(Expression<Func<Department, bool>>? Filter = null)
        {
            try
            {
                if (Filter != null)
                {

                    var result = HrDb.Department.Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = HrDb.Department.ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Department GetById(int id)
        {
            try
            {
                var result = HrDb.Department.Where(e => e.Id == id).FirstOrDefault();

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
                var oldDept = HrDb.Department.Where(e => e.Id == id).FirstOrDefault();
                if (oldDept != null)
                {

                    var result = oldDept.ToggaleStatus("Anis");
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
