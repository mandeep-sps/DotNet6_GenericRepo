using SyncLib.Model.Common;
using SyncLib.Model.Dto;
using static SyncLib.Model.Utils.Utils;

namespace SyncLib.BusinessLogic.Interface
{
    public interface IGeneralService
    {
        /// <summary>
        /// Get Data for DropDown Binding by Object Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<DropDownModel>>> GetDropDownData(DropDownListObjectType type, object? param = null);
    }
}
