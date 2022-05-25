using AutoMapper;
using SyncLib.BusinessLogic.Interface;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;
using SyncLib.Repository.Database;
using SyncLib.Repository.Interface;
using static SyncLib.Model.Utils.Utils;

namespace SyncLib.BusinessLogic
{
    public class GeneralService : IGeneralService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GeneralService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Data for DropDown Binding by Object Type
        /// </summary>
        /// <param name="ListType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<DropDownModel>>> GetDropDownData(DropDownListObjectType ListType, object? param = null)
        {
            try
            {
                IEnumerable<DropDownModel> data = ListType switch
                {
                    DropDownListObjectType.Authors => _mapper.Map<IEnumerable<DropDownModel>>(await _repository.GetAllAsync<Author>(x => !x.IsDeleted)),
                    DropDownListObjectType.Categories => _mapper.Map<IEnumerable<DropDownModel>>(await _repository.GetAllAsync<BookCategory>(x => !x.IsDeleted)),
                    DropDownListObjectType.Languages => _mapper.Map<IEnumerable<DropDownModel>>(await _repository.GetAllAsync<ScriptLanguage>(x => !x.IsDeleted)),
                    _ => new List<DropDownModel>()
                };

                return new ServiceResult<IEnumerable<DropDownModel>>(data.OrderBy(x => x.Name).ToList(), "Dropdown data");
            }
            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<DropDownModel>>(ex, ex.Message);
            }
        }
    }
}
