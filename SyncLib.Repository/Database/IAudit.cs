namespace SyncLib.Repository.Database
{
    public interface IAudit
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

    public partial class ApplicationUser : IAudit { };
    public partial class Author : IAudit { };
    public partial class Book : IAudit { };
    public partial class BookImage : IAudit { };
    public partial class BookLog : IAudit { };

    public partial class BookCategory : IAudit { };
    public partial class ScriptLanguage : IAudit { };
}
