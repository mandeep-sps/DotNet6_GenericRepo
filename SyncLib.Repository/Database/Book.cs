using System;
using System.Collections.Generic;

namespace SyncLib.Repository.Database
{
    public partial class Book
    {
        public Book()
        {
            BookImages = new HashSet<BookImage>();
            BookLogs = new HashSet<BookLog>();
        }

        public int Id { get; set; }
        public string BookName { get; set; } = null!;
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string? PublishedOn { get; set; }
        public int Edition { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual BookCategory Category { get; set; } = null!;
        public virtual ScriptLanguage Language { get; set; } = null!;
        public virtual ICollection<BookImage> BookImages { get; set; }
        public virtual ICollection<BookLog> BookLogs { get; set; }
    }
}
