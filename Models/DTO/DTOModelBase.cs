using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public abstract class DTOModelBase
    {
        public int? Id { get; set; }

        public int? LocalId { get; set; }

        public int UserId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Inactive { get; set; }
    }
}
