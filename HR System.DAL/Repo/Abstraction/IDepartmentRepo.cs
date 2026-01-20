
using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Abstraction
{
    public interface IDepartmentRepo
    {

        //Command
        bool Add(Department department);
        bool Edit(Department department);
        bool ToggaleStatus(int id);

        //Quary
        Department GetById(int id);
        List<Department> GetAll(Expression<Func<Department, bool>>? Filter = null);
        
    }
}
