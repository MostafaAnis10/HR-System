
using System.Linq;
using System.Linq.Expressions;

namespace HR_System.DAL.Repo.Implementation
{
    public class AttendanceRepo : IAttendanceRepo
    {
        private readonly HrDbContext hrDb;

        public AttendanceRepo(HrDbContext hrDb)
        {
            this.hrDb = hrDb;
        }
        public bool Add(AttendanceRecord attendance)
        {
            try
            {
                var result = hrDb.attendanceRecords.Add(attendance);
                hrDb.SaveChanges();
                if(result.Entity.Id>0)
                    return true;
                return false;
            }
            catch(Exception )
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var Attendance = GetById(id);
                if( Attendance == null ) return false;
                
                hrDb.attendanceRecords.Remove( Attendance );
                hrDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Edit(AttendanceRecord attendance)
        {
            try
            {
                var oldAttendance = hrDb.attendanceRecords.FirstOrDefault(a=>a.Id ==attendance.Id);

                if (oldAttendance != null)
                {
                    oldAttendance.AttendanceDate = attendance.AttendanceDate;
                    oldAttendance.CheckOut = attendance.CheckOut;
                    oldAttendance.CheckIn = attendance.CheckIn;

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

        public List<AttendanceRecord> GetAll(Expression<Func<AttendanceRecord, bool>>? Filter = null)
        {
            try
            {
                if (Filter != null)
                {
                    var result = hrDb.attendanceRecords.Where(Filter).ToList();
                    return result;
                }
                else
                {
                    var result = hrDb.attendanceRecords.ToList();
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public AttendanceRecord GetById(int id)
        {
            try
            {
                var result = hrDb.attendanceRecords.FirstOrDefault(a=>a.Id==id);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
