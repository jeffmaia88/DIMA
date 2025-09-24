using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests
{
   public class PagedRequest : Request
    {
        public int PageNumber { get; set; } = Configurations.DefaultPageNumber;
        public int PageSize { get; set; } = Configurations.DefaultPageSize;
    }
}
