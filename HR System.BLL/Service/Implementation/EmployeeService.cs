
namespace HR_System.BLL.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo employeeRepo;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeRepo employeeRepo ,IMapper mapper)
        {
            this.employeeRepo= employeeRepo;
            this.mapper = mapper;
        }

        public Response<CreateEmployeeVM> Create(CreateEmployeeVM model, string? fileName)
        {
            try
            {
                var emp = new Employee(model.Name, model.Salary, model.DepartmentId, fileName, "Mostafa");

                var result = employeeRepo.Add(emp);
                if (result)
                    return new Response<CreateEmployeeVM>(model, null, false);

                return new Response<CreateEmployeeVM>(model, "There was a problem saving data", true);
            }
            catch (Exception ex)
            {
                return new Response<CreateEmployeeVM>(model, ex.Message, true);
            }
        }


        public Response<bool> Delete(int id)
        {
            try
            {
                var result = employeeRepo.ToggaleStatus(id);
                if (result)
                    return new Response<bool>(true, null, false);

                return new Response<bool>(false, "Failed to delete employee", true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<EditEmployeeVM> Edit(EditEmployeeVM model)
        {
            try
            {
                // 1. هات الموظف الموجود فعلياً من قاعدة البيانات (Tracked Entity)
                var oldEmployee = employeeRepo.GetById(model.Id);
                if (oldEmployee == null)
                    return new Response<EditEmployeeVM>(model, "Employee Not Found", true);

                // 2. تحديث البيانات باستخدام ميثود الـ Update الموجودة في الـ Entity
                // هذا يضمن أن الـ Entity State أصبحت Modified
                oldEmployee.Update(model.Name, model.Salary, model.DepartmentId, "Mostafa");

                // 3. تحديث حقل الملف يدوياً لضمان عدم ضياعه أثناء الـ Mapping
                if (!string.IsNullOrEmpty(model.File))
                {
                    oldEmployee.SetFile(model.File);
                }

                // 4. حفظ التغييرات
                var result = employeeRepo.Edit(oldEmployee);

                if (result)
                    return new Response<EditEmployeeVM>(model, null, false);

                return new Response<EditEmployeeVM>(model, "Failed To Update Employee", true);
            }
            catch (Exception ex)
            {
                return new Response<EditEmployeeVM>(model, ex.Message, true);
            }
        }




        public Response<List<GetEmployeeVM>> GetActiveEmployee()
        {
            try
            {
                var result = employeeRepo.GetAll(e => e.IsDeleted == false);
                //List<GetEmployeeVM> mapp = new List<GetEmployeeVM>();
                //foreach (var item in result)
                //{
                //    mapp.Add(new GetEmployeeVM() { Id = item.Id, Name = item.Name ,CreateOn = item.CreateOn ,Salary = item.Salary });
                //}
                var mapp = mapper.Map<List<GetEmployeeVM>>(result);

                return new Response<List<GetEmployeeVM>>(mapp, null, false);


            }
            catch (Exception ex )
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);
            }
        }

        public Response<EditEmployeeVM> GetByID(int id)
        {
            try
            {
               
                
                var editEmp = employeeRepo.GetById(id);
                var mapp = mapper.Map<EditEmployeeVM>(editEmp);
                if (editEmp != null )
                    return new Response<EditEmployeeVM>(mapp, null, false);

                return new Response<EditEmployeeVM>(mapp, "Failed To Update Employee", true);

            }
            catch (Exception ex)
            {

                return new Response<EditEmployeeVM>(null, ex.Message, true);
            }
        }

        public Response<EmployeeDetailsVM> GetDetails(int id)
        {
            try
            {
                
                var empDetails = employeeRepo.GetById(id);
                var mapp = mapper.Map<EmployeeDetailsVM>(empDetails);

                if (empDetails != null)
                    return new Response<EmployeeDetailsVM>(mapp , null ,false);

                return new Response<EmployeeDetailsVM>(null , "Employee Not Found", true);

            }
            catch (Exception ex)
            {
                return new Response<EmployeeDetailsVM>(null, ex.Message, true);
            }
        }

        public Response<List<GetEmployeeVM>> GetNotActiveEmployee()
        {
            try
            {
                var employees = employeeRepo.GetAll(emp => emp.IsDeleted == true);
                //List<GetEmployeeVM> mapp = new List<GetEmployeeVM>();
                //foreach (var item in employees)
                //{
                //    mapp.Add(new GetEmployeeVM() { Id = item.Id, Name = item.Name, CreateOn = item.CreateOn, Salary = item.Salary });
                //}
                var mapp = mapper.Map<List<GetEmployeeVM>>(employees);


                return new Response<List<GetEmployeeVM>>(mapp, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);

            }
        }
    }
}
