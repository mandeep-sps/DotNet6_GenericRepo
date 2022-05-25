using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLib.BusinessLogic.Interface;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;

namespace SyncLib.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get All Books
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBooks")]
        public async Task<ApiResponseModel> GetBooks()
        {
            return GetResult(await _bookService.GetBooks());
        }


        /// <summary>
        /// Get Book By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBookById/{id}")]
        public async Task<ApiResponseModel> GetBookById(int id)
        {
            return GetResult(await _bookService.GetBookById(id));
        }

        /// <summary>
        /// Add New Book
        /// </summary>
        /// <param name="request"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        [HttpPost("AddBook")]
        public async Task<ApiResponseModel> AddBook([FromBody] BookRequestModel request, int createdBy)
        {
            return GetResult(await _bookService.AddBook(request, createdBy));
        }


        /// <summary>
        /// Update Book Details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpPut("UpdateBook")]
        public async Task<ApiResponseModel> UpdateBook([FromBody] BookRequestModel request, int id, int updatedBy)
        {
            return GetResult(await _bookService.UpdateBook(request, id, updatedBy));
        }

        /// <summary>
        /// Soft Delete Book By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedBy"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBookById")]
        public async Task<ApiResponseModel> DeleteBookById(int id, int deletedBy)
        {
            return GetResult(await _bookService.DeleteBookById(id, deletedBy));
        }
    }
}
