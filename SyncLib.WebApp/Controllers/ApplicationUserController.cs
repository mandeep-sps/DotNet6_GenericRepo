using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;
using System.Text;

namespace SyncLib.WebApp.Controllers
{
    public class SystemController : Controller
    {
        private const string UrlAuthenticate = "/api/Security/Authenticate";//POST - body params > username,password
        private readonly string _baseUrl;
        public SystemController(IOptions<ApplicationSettingsModel> options)
        {
            _baseUrl = options.Value.BaseUrl;
        }
        public IActionResult Login()
        {


            return View();
        }

        public async Task<IActionResult> SubmitLogin(LoginRequestModel login)
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_baseUrl + UrlAuthenticate, content).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponseModel>(result);
            if (apiResponse != null && apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<LoginResponseModel>(apiResponse.Data.ToString());
                HttpContext.Session.SetString("LoggedInUser", JsonConvert.SerializeObject((LoginResponseModel)data));
                return RedirectToAction("Index", "Home");
            }

            return Json(new
            {
                apiResponse?.StatusCode,
                apiResponse?.Message
            });

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "System");
        }


    }
}
