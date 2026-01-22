
namespace HR_System.BLL.Service.Implementation
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepo positionRepo;
        private readonly IMapper mapper;

        public PositionService(IPositionRepo positionRepo , IMapper mapper)
        {
            this.positionRepo = positionRepo;
            this.mapper = mapper;
        }
        public Response<CreatePositionVM> CreatePosition(CreatePositionVM model)
        {
            try
            {
                var position = new Position(model.Title, model.DefaultSalary);
                var result = positionRepo.Add(position);
                if (result)
                    return new Response<CreatePositionVM>(model, null, false);


                    return new Response<CreatePositionVM>(model, "There was a problem saving data in CreatePosition", false);
            }
            catch(Exception ex)
            {
                return new Response<CreatePositionVM>(model, ex.Message, false);
            }
        }

        public Response<bool> Delete(int id)
        {
            try
            {
                
                var result = positionRepo.Delete(id);

                if (result)
                    return new Response<bool>(true, null, false);

                return new Response<bool>(false, "Failed to delete position", true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<EditPositionVM> EditPosition(EditPositionVM model)
        {
            try
            {
                var position = positionRepo.GetById(model.Id);

                if (position == null)
                    return new Response<EditPositionVM>(model, "Position Not Found", true);

                position.Title = model.Title;
                position.DefaultSalary = model.DefaultSalary;

                
                var result = positionRepo.Edit(position);

                if (result)
                    return new Response<EditPositionVM>(model, null, false);

                return new Response<EditPositionVM>(model, "Failed to update database", true);
            }
            catch (Exception ex)
            {
                return new Response<EditPositionVM>(model, ex.Message, true);
            }
        }

        public Response<EditPositionVM> GetByID(int id)
        {
            try
            {
                var result = positionRepo.GetById(id);
                var mapp = mapper.Map<EditPositionVM>(result);
                if (result != null)
                    return new Response<EditPositionVM>(mapp, null, false);

                return new Response<EditPositionVM>(mapp, "Failed To Update Position", true);


            }
            catch (Exception ex)
            {
                return new Response<EditPositionVM>(null, ex.Message, true);
            }
        }

        public Response<List<GetPositionVM>> GetPosition()
        {
            try
            {
                var result = positionRepo.GetAll();
                
                var mapp = mapper.Map<List<GetPositionVM>>(result);

                return new Response<List<GetPositionVM>>(mapp, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetPositionVM>>(null, ex.Message, true);
            }
        }
    }
}
