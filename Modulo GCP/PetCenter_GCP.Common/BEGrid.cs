﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Common
{
    public class BEGrid
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int RowsSize { get; set; }
        public int RecordCount { get; set; }
    }
}