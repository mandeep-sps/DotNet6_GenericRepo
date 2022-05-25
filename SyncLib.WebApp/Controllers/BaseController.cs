using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;
using SyncLib.WebApp.Models.Extensions;
using System.Text;
using static SyncLib.Model.Utils.Utils;

namespace SyncLib.WebApp.Controllers
{
    [MiddlewareFilter(typeof(AppJwtAuthenticationMiddlewarePipeline))]
    public class BaseController : Controller
    {
        private readonly ApplicationSettingsModel _appSettings;
        private readonly HttpClient client;
        public BaseController(IOptions<ApplicationSettingsModel> options)
        {
            _appSettings = options.Value;
            client = new HttpClient();
        }

        protected async Task<ApiResponseModel> SendRequestAsync(string apiEndPoint, HttpAction action = HttpAction.Get, dynamic body = null)
        {
            var sessionString = HttpContext.Session.GetString("LoggedInUser") ?? "";
            var session = sessionString.ToObject<LoginResponseModel>();
            if (session is not null && !string.IsNullOrEmpty(session.Token))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + session.Token);

            // Authenticate with HMAC
            HttpResponseMessage response = action switch
            {
                HttpAction.Get => await GetAsync(apiEndPoint, _appSettings.BaseUrl),
                HttpAction.Post => await CreateAsync(apiEndPoint, body, _appSettings.BaseUrl),
                HttpAction.Put => await UpdateAsync(apiEndPoint, body, _appSettings.BaseUrl),
                HttpAction.Delete => await DeleteAsync(apiEndPoint, _appSettings.BaseUrl),
                _ => new HttpResponseMessage()
            };



            // Read result into string
            var result = await response.Content.ReadAsStringAsync();

            // Deserialize into ApiResponseHelper Response
            ApiResponseModel data = result.ToObject<ApiResponseModel>();
            if (data == null && response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                data = new ApiResponseModel(System.Net.HttpStatusCode.Unauthorized, "Session Expired! Please login again to continue.", null, null);

            return data;
        }


        // GET Request
        private async Task<HttpResponseMessage> GetAsync(string endpoint, string apiBaseAddress)
        {
            return await client.GetAsync(apiBaseAddress + endpoint).ConfigureAwait(false);
        }

        // CREATE Request
        private async Task<HttpResponseMessage> CreateAsync(string endpoint, dynamic request, string apiBaseAddress)
        {
            // Serialize HttpRequestMessage to JSON
            var content = new StringContent(  JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Post
            return await client.PostAsync(apiBaseAddress + endpoint, content).ConfigureAwait(false);
        }

        // UPDATE Request
        private async Task<HttpResponseMessage> UpdateAsync(string endpoint, dynamic request, string apiBaseAddress)
        {
            // Serialize HttpRequestMessage to JSON
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Put
            return await client.PutAsync(apiBaseAddress + endpoint, content).ConfigureAwait(false);
        }

        // DELETE Request
        private async Task<HttpResponseMessage> DeleteAsync(string endpoint, string apiBaseAddress)
        {
            // Delete
            return await client.DeleteAsync(apiBaseAddress + endpoint).ConfigureAwait(false);
        }


        protected JsonResult GetResult<T>(ApiResponseModel response)
        {
            dynamic data = null;
            if (response.Data != null)
                if (response.Data is bool)
                    data = (bool)response.Data;
                else
                    data = JsonConvert.DeserializeObject<T>(response.Data.ToString());

            return Json(new
            {
                response.StatusCode,
                response.Success,
                response.Message,
                Error = response.Exception,
                Data = data
            });
        }

    }
}
