using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;

namespace SyncLib.WebApp.Controllers
{
    public class BookController : BaseController
    {
        //API Endpoints
        private const string UrlGetAllBooks = "/api/Book/GetBooks";
        private const string UrlGetBookById = "/api/Book/GetBookById";
        private const string UrlAddBook = "/api/Book/AddBook";
        private const string UrlUpdateBookById = "/api/Book/UpdateBook";
        private const string UrlDeleteBookById = "/api/Book/DeleteBookById";

        //API Urls for DataList
        private const string UrlDataListAuthors = "/api/General/GetAuthorsDropDown";
        private const string UrlDataListCategories = "/api/General/GetBookCategoriesDropDown";
        private const string UrlDataListLanguages = "/api/General/GetLanguagesDropDown";

        public BookController(IOptions<ApplicationSettingsModel> options) : base(options) { }

        #region Views
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Data Transfer Actions
        public async Task<JsonResult> GetBooks()
        {
            return GetResult<IEnumerable<BookResponseModel>>(await SendRequestAsync(UrlGetAllBooks));
        }

        public async Task<JsonResult> GetBookById(int bookId)
        {
            return GetResult<BookResponseModel>(await SendRequestAsync($"{UrlGetBookById}/{bookId}"));
        }


        public async Task<JsonResult> AddBook(BookRequestModel request)
        {
            return GetResult<BookResponseModel>(await SendRequestAsync($"{UrlAddBook}?createdBy=1", Model.Utils.Utils.HttpAction.Post, request));
        }

        public async Task<JsonResult> UpdateBookById(BookRequestModel request, int bookId)
        {
            return GetResult<BookResponseModel>(await SendRequestAsync($"{UrlUpdateBookById}?id={bookId}&updatedBy=1", Model.Utils.Utils.HttpAction.Put, request));
        }


        public async Task<JsonResult> DeleteBookById(int bookId)
        {
            return GetResult<BookResponseModel>(await SendRequestAsync($"{UrlDeleteBookById}?id={bookId}&deletedBy=1", Model.Utils.Utils.HttpAction.Delete));
        }


        #endregion

        #region DataLists
        public async Task<JsonResult> GetAuthorsDataList()
        {
            return GetResult<IEnumerable<DropDownModel>>(await SendRequestAsync(UrlDataListAuthors));
        }

        public async Task<JsonResult> GetCategoriesDataList()
        {
            return GetResult<IEnumerable<DropDownModel>>(await SendRequestAsync(UrlDataListCategories));
        }


        public async Task<JsonResult> GetLanguagesDataList()
        {
            return GetResult<IEnumerable<DropDownModel>>(await SendRequestAsync(UrlDataListLanguages));
        }
        #endregion
    }
}
