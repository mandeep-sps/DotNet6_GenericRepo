using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLib.BusinessLogic.Interface;
using SyncLib.Model.Common;
using static SyncLib.Model.Utils.Utils;

namespace SyncLib.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GeneralController : BaseController
    {
        private readonly IGeneralService _generalService;

        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        /// <summary>
        /// Get Authors Data for Dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAuthorsDropDown")]
        public async Task<ApiResponseModel> GetAuthorsDropDown()
        {
            return GetResult(await _generalService.GetDropDownData(DropDownListObjectType.Authors));
        }

        /// <summary>
        /// Get Book Categories Data for Dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBookCategoriesDropDown")]
        public async Task<ApiResponseModel> GetBookCategoriesDropDown()
        {
            return GetResult(await _generalService.GetDropDownData(DropDownListObjectType.Categories));
        }

        /// <summary>
        /// Get Language Data for Dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLanguagesDropDown")]
        public async Task<ApiResponseModel> GetLanguagesDropDown()
        {
            return GetResult(await _generalService.GetDropDownData(DropDownListObjectType.Languages));
        }
    }
}
