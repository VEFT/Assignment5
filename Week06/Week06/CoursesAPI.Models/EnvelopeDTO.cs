using System;
using System.Collections.Generic;

namespace CoursesAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnvelopeDTO<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public class PagingInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public int PageNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int PageSize { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int PageCount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public long TotalRecordCount { get; set; }

        }

        /// <summary>
        /// 
        /// </summary>
        public List<T> Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public PagingInfo Paging { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecordCount"></param>
        public EnvelopeDTO(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Data = new List<T>(items);
            Paging = new PagingInfo
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecordCount = totalRecordCount,
                PageCount = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0
            };
        }
    }
}
