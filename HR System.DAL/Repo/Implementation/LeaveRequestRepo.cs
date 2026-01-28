
using System.Linq;
using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Implementation
{
    public class LeaveRequestRepo : ILeaveRequestRepo
    {
        private readonly HrDbContext hrDb;

        public LeaveRequestRepo(HrDbContext hrDb) 
        {
            this.hrDb = hrDb;
        }
        public bool Add(LeaveRequest leaveRequest)
        {
            try
            {
                var result = hrDb.leaveRequests.Add(leaveRequest);
                hrDb.SaveChanges();
                if (result.Entity.Id > 0)
                    return true;
                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = hrDb.leaveRequests.FirstOrDefault(e=>e.Id == id);
                if(result == null) return false;
                hrDb.Remove(result);
                hrDb.SaveChanges();
                return true;

            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool Edit(LeaveRequest leaveRequest)
        {
            try
            {
              var result = hrDb.leaveRequests.FirstOrDefault(e=>e.Id==leaveRequest.Id);
                if(result == null) return false;
                if(result !=null)
                {
                    result.FromDate = leaveRequest.FromDate;
                    result.ToDate = leaveRequest.ToDate;
                    result.Status = leaveRequest.Status;
                    result.LeaveType = leaveRequest.LeaveType;
                    hrDb.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<LeaveRequest> GetAll(Expression<Func<LeaveRequest, bool>>? Filter = null)
        {
            try
            {
                if (Filter != null)
                {
                    var result = hrDb.leaveRequests.Include(x => x.Employee).Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = hrDb.leaveRequests.Include(x => x.Employee).ToList();
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public LeaveRequest GetById(int id)
        {
            try
            {
                var result = hrDb.leaveRequests.Include(e=>e.Employee).FirstOrDefault(a => a.Id == id);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
