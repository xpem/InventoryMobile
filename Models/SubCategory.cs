using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SubCategory : ModelBase
    {
        public string? Name { get; set; }

        public string? IconName { get; set; }

        public int? SystemDefault { get; set; }

        public int CategoryId { get; set; }


    }
}
