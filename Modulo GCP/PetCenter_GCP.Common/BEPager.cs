using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Common
{
    public class BEPager
    {
        public int RecordCount { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}