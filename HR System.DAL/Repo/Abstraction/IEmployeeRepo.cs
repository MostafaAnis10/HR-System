using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo
    {
        //Command
        bool Add(Employee employee);
        bool Edit(Employee employee);
        bool ToggaleStatus(int id);

        //Quary
        Employee GetById(int id);
        List<Employee> GetAll(Expression<Func<Employee, bool>>? Filter = null);
        List<Employee> GetActiveEmployee();

    }
}
