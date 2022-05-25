namespace SyncLib.Model.Dto
{
    /// <summary>
    /// Book Response Model
    /// </summary>
    public class BookResponseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Book Name
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// Author Name
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Book Category
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Book script language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Publish Date
        /// </summary>
        public string PublishedOn { get; set; }

        /// <summary>
        /// Book Edition
        /// </summary>
        public int Edition { get; set; }

    }

    /// <summary>
    /// Book Request Model
    /// </summary>
    public class BookRequestModel
    {
        /// <summary>
        /// Book Name
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// Author Id
        /// </summary>
        public int AuthorId { get; set; }


        /// <summary>
        /// Author
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Book Category Id
        /// </summary>
        public int CategoryId { get; set; }


        /// <summary>
        /// Book Category
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Book Language Id
        /// </summary>
        public int LanguageId { get; set; }


        /// <summary>
        /// Book Language
        /// </summary>
        public string LanguageName { get; set; }

        /// <summary>
        /// Publish Date
        /// </summary>
        public string PublishedOn { get; set; }

        /// <summary>
        /// Book Edition
        /// </summary>
        public int Edition { get; set; }

    }
}