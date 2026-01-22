using HR_System.BLL.ModelVM.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_System.BLL.Service.Abstraction
{
    public interface IPositionService
    {
        Response<List<GetPositionVM>> GetPosition();
        Response<EditPositionVM> EditPosition(EditPositionVM model);
        Response<CreatePositionVM> CreatePosition(CreatePositionVM model);
        Response<EditPositionVM> GetByID(int id);
        Response<bool> Delete(int id);
    }
}
