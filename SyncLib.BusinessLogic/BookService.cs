using AutoMapper;
using SyncLib.BusinessLogic.Interface;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;
using SyncLib.Repository.Database;
using SyncLib.Repository.Interface;

namespace SyncLib.BusinessLogic
{
    public class BookService : IBookService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor BookService
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public BookService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Books
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<BookResponseModel>>> GetBooks()
        {
            try
            {
                
                var data = await _repository.GetAllAsync<Book>(x => !x.IsDeleted,
                    A => A.Author,
                    C => C.Category,
                    L => L.Language);

                var responseModel = _mapper.Map<IEnumerable<BookResponseModel>>(data);
                return new ServiceResult<IEnumerable<BookResponseModel>>(responseModel, "Book List");
            }

            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<BookResponseModel>>(ex, ex.Message);
            }
        }

        /// <summary>
        /// Get Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<BookResponseModel>> GetBookById(int id)
        {
            try
            {
                var data = await _repository.GetAsync<Book>(x => x.Id.Equals(id) && !x.IsDeleted,
                    A => A.Author,
                    C => C.Category,
                    L => L.Language);

                if (data != null)
                    return new ServiceResult<BookResponseModel>(_mapper.Map<BookResponseModel>(data), "Book Details");
                else
                    return new ServiceResult<BookResponseModel>(data: null, "Book not found!");

            }

            catch (Exception ex)
            {
                return new ServiceResult<BookResponseModel>(ex, ex.Message);
            }
        }

        /// <summary>
        /// Add new book
        /// </summary>
        /// <param name="request"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public async Task<ServiceResult<BookResponseModel>> AddBook(BookRequestModel request, int createdBy)
        {
            try
            {
                if (await _repository.IsExistsAsync<Book>(x => !x.IsDeleted
                                                            && x.BookName.Equals(request.BookName)
                                                            && x.AuthorId.Equals(request.AuthorId)
                                                            && x.LanguageId.Equals(request.LanguageId)
                                                            && x.CategoryId.Equals(request.CategoryId)
                                                            && x.Edition.Equals(request.Edition)))

                    return new ServiceResult<BookResponseModel>(data: null, "This book is already in your collection!");

                //Adding author,category and language if not exists
                await AddSubModules(request, createdBy);

                var dbObject = _mapper.Map<Book>(request);
                await _repository.AddAsync(dbObject, createdBy);
                return new ServiceResult<BookResponseModel>(_mapper.Map<BookResponseModel>(dbObject), "Hurray! Collection updated with a pond of knowledge!");
            }
            catch (Exception ex)
            {
                return new ServiceResult<BookResponseModel>(ex, ex.Message);
            }
        }

        /// <summary>
        /// Update book details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<ServiceResult<BookResponseModel>> UpdateBook(BookRequestModel request, int id, int updatedBy)
        {
            try
            {
                if (await _repository.IsExistsAsync<Book>(x => !x.IsDeleted
                                                            && !x.Id.Equals(id)
                                                            && x.BookName.Equals(request.BookName)
                                                            && x.AuthorId.Equals(request.AuthorId)
                                                            && x.LanguageId.Equals(request.LanguageId)
                                                            && x.CategoryId.Equals(request.CategoryId)
                                                            && x.Edition.Equals(request.Edition)))

                    return new ServiceResult<BookResponseModel>(data: null, "This book is already exists!");

                var dbObject = await _repository.GetAsync<Book>(x => x.Id.Equals(id) && !x.IsDeleted);
                if (dbObject is null)
                    return new ServiceResult<BookResponseModel>(data: null, "Book not found!");
                else
                {

                    //Adding author,category and language if not exists
                    await AddSubModules(request, updatedBy);


                    _mapper.Map(request, dbObject);
                    await _repository.Update(dbObject, updatedBy);
                    return new ServiceResult<BookResponseModel>(_mapper.Map<BookResponseModel>(dbObject), "Book has been updated with the correct data!");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<BookResponseModel>(ex, ex.Message);
            }
        }

        /// <summary>
        /// Soft Delete Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedBy"></param>
        /// <returns></returns>
        public async Task<ServiceResult<bool>> DeleteBookById(int id, int deletedBy)
        {
            try
            {
                var dbObject = await _repository.GetAsync<Book>(x => x.Id.Equals(id) && !x.IsDeleted);
                if (dbObject != null)
                {
                    await _repository.Remove(dbObject, deletedBy);
                    return new ServiceResult<bool>(true, "Book removed successfully!");
                }
                else
                    return new ServiceResult<bool>(false, "Book not found!");

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(ex, ex.Message);
            }
        }
        /// <summary>
        /// Private Method for Sub Modules
        /// </summary>
        /// <param name="request"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        private async Task AddSubModules(BookRequestModel request, int createdBy)
        {
            if (request.AuthorId.Equals(0))
                request.AuthorId = await AddNewAuthor(request.AuthorName, createdBy);

            if (request.CategoryId.Equals(0))
                request.CategoryId = await AddNewCategory(request.CategoryName, createdBy);

            if (request.LanguageId.Equals(0))
                request.LanguageId = await AddNewLanguage(request.LanguageName, createdBy);
        }

        /// <summary>
        /// Method for Adding new Author
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        private async Task<int> AddNewAuthor(string name, int createdBy)
        {
            var author = new Author() { AuthorName = name };
            await _repository.AddAsync(author, createdBy);
            return author.Id;
        }

        /// <summary>
        /// Method for adding new Category
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        private async Task<int> AddNewCategory(string name, int createdBy)
        {
            var category = new BookCategory() { Category = name };
            await _repository.AddAsync(category, createdBy);
            return category.Id;
        }


        private async Task<int> AddNewLanguage(string name, int createdBy)
        {
            var language = new ScriptLanguage() { LanguageName = name };
            await _repository.AddAsync(language, createdBy);
            return language.Id;
        }
    }
}