
using System.Collections.Generic;
using User.Model.Models;

namespace User.Model.Http.Response
{
    public class HttpApiResponse
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }

        public int TotalPages { get; set; }

        public List<UserEntity> Data { get; set; }

        public HttpApiResponse()
        {
            Data = new List<UserEntity>();
        }
    }
}
