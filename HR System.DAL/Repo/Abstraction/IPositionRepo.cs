using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR_System.DAL.Repo.Abstraction
{
    public interface IPositionRepo
    {
        //Command
        bool Add(Position position);
        bool Edit(Position position);
        bool Delete(int id);

        //Quary
        Position GetById(int id);
        List<Position> GetAll(Expression<Func<Position, bool>>? Filter = null);
    }
}
