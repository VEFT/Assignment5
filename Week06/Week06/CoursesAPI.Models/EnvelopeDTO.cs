using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class EnvelopeDTO<T>
    {
        public T items { get; set; }

        public class PagingInfo
        {
            public int PageCount { get; set; }
        }

        public PagingInfo Paging { get; set; }
    }
}
