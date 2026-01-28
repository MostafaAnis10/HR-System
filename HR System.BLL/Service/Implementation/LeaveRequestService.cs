using HR_System.BLL.ModelVM.LeaveRequest;

namespace HR_System.BLL.Service.Implementation
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepo leaveRequestRepo;
        private readonly IMapper mapper;

        public LeaveRequestService(ILeaveRequestRepo leaveRequestRepo , IMapper mapper)
        {
            this.leaveRequestRepo = leaveRequestRepo;
            this.mapper = mapper;
        }
        public Response<bool> ChangeStatus(int id, string status)
        {
            try
            {
                var entity = leaveRequestRepo.GetById(id);
                if (entity == null)
                    return new Response<bool>(false, "Leave Request Not Found", true);

                if (entity.Status != LeaveStatus.Pending)
                    return new Response<bool>(false, "Request already processed", true);

                if (!Enum.TryParse<LeaveStatus>(status, true, out var newStatus))
                    return new Response<bool>(false, "Invalid Status", true);

                entity.Status = newStatus;

                var success = leaveRequestRepo.Edit(entity);
                if (!success)
                    return new Response<bool>(false, "Failed to change status", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<bool> Create(CreateLeaveRequestVM model)
        {
            try
            {
                if (model.FromDate > model.ToDate)
                    return new Response<bool>(false, "FromDate must be before ToDate", true);

                var entity = new LeaveRequest
                {
                    EmployeeId = model.EmployeeId,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    LeaveType = model.LeaveType,
                    Status = LeaveStatus.Pending
                };

                var success = leaveRequestRepo.Add(entity);
                if (!success)
                    return new Response<bool>(false, "Failed to save leave request", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<bool> Delete(int id)
        {
            try
            {
                var entity = leaveRequestRepo.GetById(id);
                if (entity == null)
                    return new Response<bool>(false, "Leave Request Not Found", true);

                if (entity.Status != LeaveStatus.Pending)
                    return new Response<bool>(false, "Cannot delete processed request", true);

                var success = leaveRequestRepo.Delete(id);
                if (!success)
                    return new Response<bool>(false, "Failed to delete leave request", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<List<GetLeaveRequestVM>> GetAll()
        {
            try
            {
                var list = leaveRequestRepo.GetAll();
                var result = mapper.Map<List<GetLeaveRequestVM>>(list);

                return new Response<List<GetLeaveRequestVM>>(result, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetLeaveRequestVM>>(null, ex.Message, true);
            }
        }

        public Response<DetailsLeaveRequestVM> GetById(int id)
        {
            try
            {
                var entity = leaveRequestRepo.GetById(id);
                if (entity == null)
                    return new Response<DetailsLeaveRequestVM>(null, "Leave Request Not Found", true);

                var result = mapper.Map<DetailsLeaveRequestVM>(entity);
                return new Response<DetailsLeaveRequestVM>(result, null, false);
            }
            catch (Exception ex)
            {
                return new Response<DetailsLeaveRequestVM>(null, ex.Message, true);
            }
        }

        public Response<bool> Update(int id, CreateLeaveRequestVM model)
        {
            try
            {
                var entity = leaveRequestRepo.GetById(id);
                if (entity == null)
                    return new Response<bool>(false, "Leave Request Not Found", true);

                if (entity.Status != LeaveStatus.Pending)
                    return new Response<bool>(false, "Cannot edit processed request", true);

                if (model.FromDate > model.ToDate)
                    return new Response<bool>(false, "Invalid date range", true);

                entity.FromDate = model.FromDate;
                entity.ToDate = model.ToDate;
                entity.LeaveType = model.LeaveType;

                var success = leaveRequestRepo.Edit(entity);
                if (!success)
                    return new Response<bool>(false, "Failed to update leave request", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }
    }
}
