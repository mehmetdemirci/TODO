using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.TODO.Domain
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.CreatedDate = DateTime.Now;
            this.Deleted = false;
            this.Active = true;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public Nullable<DateTime> ModifiedDate { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
