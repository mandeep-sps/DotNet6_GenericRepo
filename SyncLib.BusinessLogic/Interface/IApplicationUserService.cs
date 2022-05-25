using SyncLib.Model.Common;
using SyncLib.Model.Dto;

namespace SyncLib.BusinessLogic.Interface
{
    public interface IApplicationUserService
    {
        Task<ServiceResult<LoginResponseModel>> GetLogin(LoginRequestModel request);
    }
}
