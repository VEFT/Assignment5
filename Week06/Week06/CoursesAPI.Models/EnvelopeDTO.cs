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
        public T items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class PagingInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public int PageCount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int PageSize { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int PageNumber { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int TotalNumberOfItems { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PagingInfo Paging { get; set; }
    }
}
