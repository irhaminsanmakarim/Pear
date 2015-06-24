using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Data.Entities
{
    public class BaseEntity
    {
        private DateTime? _createdDate = null;
        private DateTime? _updatedDate = null;
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate.HasValue
                           ? this._createdDate.Value
                           : DateTime.Now;
            }

            set { _createdDate = value; }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return _updatedDate.HasValue
                   ? _updatedDate.Value
                   : DateTime.Now;
            }

            set { _updatedDate = value; }
        }

        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
    }
}
