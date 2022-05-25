using System;
using System.Collections.Generic;

namespace SyncLib.Repository.Database
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            BookLogs = new HashSet<BookLog>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int UserRoleId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ApplicationUserRole IdNavigation { get; set; } = null!;
        public virtual ICollection<BookLog> BookLogs { get; set; }
    }
}
