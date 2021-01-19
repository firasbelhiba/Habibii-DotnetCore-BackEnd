using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habibii.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            // if it s greater than the max page size thant it will be set automaticcly to the maximum page size
            set { pageSize = (value > MaxPageSize ) ? MaxPageSize : value ; }
        }

        public int UserId { get; set; }

        public string Gender { get; set; }

        public int MinAge { get; set; } = 18;

        public int MaxAge { get; set; } = 99;

        public string OrderBy { get; set; }
    }
}
