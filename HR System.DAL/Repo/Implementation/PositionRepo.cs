using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR_System.DAL.Repo.Implementation
{
    public class PositionRepo : IPositionRepo
    {
        private readonly HrDbContext hrDb;

        public PositionRepo(HrDbContext hrDb)
        {
            this.hrDb = hrDb;
        }

        public bool Add(Position position)
        {
            try
            {
                var result = hrDb.positions.Add(position);
                hrDb.SaveChanges();
                if (result.Entity.Id > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var position = GetById(id);
                if (position == null) return false;

                hrDb.positions.Remove(position);
                int rowsAffected = hrDb.SaveChanges();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Edit(Position position)
        {
            try
            {
                var oldPostion = hrDb.positions.FirstOrDefault(p => p.Id == position.Id);

                if(oldPostion !=null)
                {
                    oldPostion.Title = position.Title;
                    oldPostion.DefaultSalary = position.DefaultSalary;

                    int rowsAffected = hrDb.SaveChanges();
                    return rowsAffected > 0;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Position> GetAll(Expression<Func<Position, bool>>? Filter = null)
        {
            try
            {
                if (Filter != null)
                {

                    var result = hrDb.positions.Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = hrDb.positions.ToList();
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Position GetById(int id)
        {
            try
            {
                var result = hrDb.positions.FirstOrDefault(p=>p.Id == id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
