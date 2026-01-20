
namespace HR_System.BLL.Service.Implementation
{
    public class DepartmentService :IDepartmentService
    {
        private readonly IDepartmentRepo departmentRepo;
        private readonly IMapper mapper;

        public DepartmentService(IDepartmentRepo departmentRepo ,IMapper  mapper)
        {
            this.departmentRepo = departmentRepo;
            this.mapper = mapper;
        }

        public Response<CreateDepartementVM> CreateDepartment(CreateDepartementVM model)
        {
            try
            {
                var depatement = new Department(model.Name , "Mostafa Anis");
                var result = departmentRepo.Add(depatement);
                if (result)
                    return new Response<CreateDepartementVM>(model, null, false);

                return new Response<CreateDepartementVM>(model, "There was a problem saving data in CreateDepartement", true);
            }
            catch (Exception ex)
            {
                return new Response<CreateDepartementVM>(model ,ex.Message, true);

            }
        }

        public Response<bool> Delete(int id)
        {
            try
            {
                var department = departmentRepo.GetById(id);
                if (department == null)
                    return new Response<bool>(false, "Department Not Found", true);

                var result = departmentRepo.ToggaleStatus(id); // أو الـ logic اللي عندك في الـ Repo
                return new Response<bool>(result, null, !result);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<EditDepartementVM> EditDepartment(EditDepartementVM model)
        {
            try
            {
                var oldDepartement = departmentRepo.GetById(model.Id);
                if(oldDepartement == null)
                    return new Response<EditDepartementVM>(model , "Departement Not Found" , true);

                oldDepartement.Update(model.Name, "Mostafa Anis");

                var result = departmentRepo.Edit(oldDepartement);
                if (result)
                    return new Response<EditDepartementVM>(model, null, false);


                    return new Response<EditDepartementVM>(model, "Failed To Update Departement", true);

            }
            catch (Exception ex)
            {
                return new Response<EditDepartementVM>(model, ex.Message, true);

            }
        }

        public Response<EditDepartementVM> GetByID(int id)
        {
            try
            {
                var result = departmentRepo.GetById(id);
                var mapp = mapper.Map<EditDepartementVM>(result);
                if(result!=null)
                return new Response<EditDepartementVM>(mapp, null, false);

                return new Response<EditDepartementVM>(mapp, "Failed To Update Employee", true);


            }
            catch (Exception ex)
            {
                return new Response<EditDepartementVM>(null, ex.Message, true);

            }
        }

        public Response<List<GetDepartmentVM>> GetDepartment()
        {
            try
            {
                var result = departmentRepo.GetAll();
                var mapp = mapper.Map<List<GetDepartmentVM>>(result);
                return new Response<List<GetDepartmentVM>>(mapp, null, false);
            }
            catch ( Exception ex )
            {
                return new Response<List<GetDepartmentVM>>(null, ex.Message, true);

            }
        }


    }
}
