using System;
using System.Collections.Generic;

namespace SyncLib.Repository.Database
{
    public partial class ApplicationUserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
