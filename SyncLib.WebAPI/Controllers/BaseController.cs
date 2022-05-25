using Microsoft.AspNetCore.Mvc;
using SyncLib.Model.Common;
using System.Net;

namespace SyncLib.WebAPI.Controllers
{
    public class BaseController : Controller
    {

        /// <summary> new key generate kr k 1 bar testing kr lo
        /// Get Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceResult"></param>
        /// <returns></returns>
        public ApiResponseModel GetResult<T>(ServiceResult<T> serviceResult)
        {
            return new ApiResponseModel(
                serviceResult.HasValidationError ? HttpStatusCode.Conflict :
                             serviceResult.Exception != null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
                serviceResult.Message,
                serviceResult.Exception,
                serviceResult.Data);
        }
    }
}
