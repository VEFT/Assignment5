using System;
using System.Collections.Generic;

namespace CoursesAPI.Models
{
    /// <summary>
    /// When requesting a list of courses, the application should return max 10 courses. 
    /// The list is returned via this envelope class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnvelopeDTO<T>
    {
        /// <summary>
        /// An object that stores information about paging,
        /// i.e. the number of the current page, the number of items 
        /// on each page, number of total pages, 
        /// and the total number of items in the collection
        /// </summary>
        public class PagingInfo
        {
            /// <summary>
            /// Stores a 1-based index of the current page being returned
            /// </summary>
            public int PageNo { get; set; }

            /// <summary>
            /// Stores the number of items in each page 
            /// (10 in our case)
            /// </summary>
            public int PageSize { get; set; }

            /// <summary>
            /// Stores the number of pages
            /// </summary>
            public int PageCount { get; set; }

            /// <summary>
            /// Stores the total number of items in the collection
            /// </summary>
            public long TotalRecordCount { get; set; }
        }

        /// <summary>
        /// A list of items in our collection 
        /// (courses in our case)
        /// </summary>
        public List<T> Data { get; private set; }

        /// <summary>
        /// The paging information of our page
        /// </summary>
        public PagingInfo Paging { get; private set; }

        /// <summary>
        /// The envelope object that includes all the necessary information,
        /// both our data, and the structure of our page.
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
