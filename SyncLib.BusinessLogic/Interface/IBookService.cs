using SyncLib.Model.Common;
using SyncLib.Model.Dto;

namespace SyncLib.BusinessLogic.Interface
{
    public interface IBookService
    {
        /// <summary>
        /// Get Books
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<BookResponseModel>>> GetBooks();

        /// <summary>
        /// Get Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<BookResponseModel>> GetBookById(int id);

        /// <summary>
        /// Add New Book
        /// </summary>
        /// <param name="request"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        Task<ServiceResult<BookResponseModel>> AddBook(BookRequestModel request, int createdBy);

        /// <summary>
        /// Update Book Details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ServiceResult<BookResponseModel>> UpdateBook(BookRequestModel request, int id, int updatedBy);

        /// <summary>
        /// Soft Delete Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedBy"></param>
        /// <returns></returns>
        Task<ServiceResult<bool>> DeleteBookById(int id, int deletedBy);
    }
}
