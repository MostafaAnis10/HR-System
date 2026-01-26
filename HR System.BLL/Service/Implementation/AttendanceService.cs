
using HR_System.BLL.ModelVM.Attendance;
using HR_System.DAL.Repo.Abstraction;
using HR_System.DAL.Repo.Implementation;

namespace HR_System.BLL.Service.Implementation
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo attendanceRepo;
        private readonly IMapper mapper;

        public AttendanceService(IAttendanceRepo attendanceRepo, IMapper mapper)
        {
            this.attendanceRepo = attendanceRepo;
            this.mapper = mapper;
        }

        
        public Response<List<GetAttendanceVM>> GetAll()
        {
            try
            {
                var result = attendanceRepo.GetAll();
                var mapp = mapper.Map<List<GetAttendanceVM>>(result);
                return new Response<List<GetAttendanceVM>>(mapp, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetAttendanceVM>>(null, ex.Message, true);
            }
        }

        
        public Response<DetailsAttendanceVM> GetByID(int id)
        {
            try
            {
                var result = attendanceRepo.GetById(id);
                if (result == null)
                    return new Response<DetailsAttendanceVM>(null, "Not Found", true);

                var mapp = mapper.Map<DetailsAttendanceVM>(result);
                return new Response<DetailsAttendanceVM>(mapp, null, false);
            }
            catch (Exception ex)
            {
                return new Response<DetailsAttendanceVM>(null, ex.Message, true);
            }
        }

        
        public Response<EditAttendanceVM> Edit(EditAttendanceVM model)
        {
            try
            {
                var attendEntity = attendanceRepo.GetById(model.Id);
                if (attendEntity == null)
                    return new Response<EditAttendanceVM>(model, "Attendance Not Found", true);

                //map
                attendEntity.CheckOut = model.CheckOut;

                var result = attendanceRepo.Edit(attendEntity);
                if (result)
                    return new Response<EditAttendanceVM>(model, null, false);

                return new Response<EditAttendanceVM>(model, "Failed to update database", true);
            }
            catch (Exception ex)
            {
                return new Response<EditAttendanceVM>(model, ex.Message, true);
            }
        }

        
        public Response<bool> Delete(int id)
        {
            try
            {
                var result = attendanceRepo.Delete(id);
                if (result)
                    return new Response<bool>(true, null, false);

                return new Response<bool>(false, "Failed to delete Attendance", true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

       
        public Response<CreateAttendanceVM> Create(CreateAttendanceVM model)
        {
            try
            {

                var attend = new AttendanceRecord(
                DateTime.Now,
                null,
                DateTime.Today,
                model.EmployeeId);


                var result = attendanceRepo.Add(attend);
                if (result)
                    return new Response<CreateAttendanceVM>(model, null, false);

                return new Response<CreateAttendanceVM>(model, "Problem saving data", true);
            }
            catch (Exception ex)
            {
                return new Response<CreateAttendanceVM>(model, ex.Message, true);
            }
        }

        public Response<bool> CheckOut(int attendanceId)
        {
            try
            {
                var attendance = attendanceRepo.GetById(attendanceId);
                if (attendance == null)
                    return new Response<bool>(false, "Attendance not found", true);

                if (attendance.CheckOut != null)
                    return new Response<bool>(false, "Already checked out", true);

                attendance.RegisterCheckOut();
                attendanceRepo.Edit(attendance);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }
    }
}
