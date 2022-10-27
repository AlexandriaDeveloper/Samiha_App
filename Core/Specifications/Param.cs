using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class Param
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 0;
        public bool IsPagination { get; set; } = true;

        private int _pageSize = 30;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = _pageSize > MaxPageSize ? MaxPageSize : value; }
        }
        public string Sort { get; set; } = "";
        public string Order { get; set; } = "";
    }
}