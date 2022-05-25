using System;
using System.Collections.Generic;

namespace SyncLib.Repository.Database
{
    public partial class BookLog
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime IssuedOn { get; set; }
        public int IssueTo { get; set; }
        public DateTime? RecievedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual ApplicationUser IssueToNavigation { get; set; } = null!;
    }
}
