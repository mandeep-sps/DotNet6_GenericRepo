using System;
using System.Collections.Generic;

namespace SyncLib.Repository.Database
{
    public partial class BookImage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public byte[] BookImage1 { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
