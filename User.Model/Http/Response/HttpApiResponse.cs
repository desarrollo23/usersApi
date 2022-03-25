
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using User.Model.DTOs;
using User.Model.Models;

namespace User.Model.Http.Response
{
    public class HttpApiResponse
    {
        public int Page { get; set; }

        public int Per_Page { get; set; }
        public int Total { get; set; }

        public int Total_Pages { get; set; }

        public List<UserApi> Data { get; set; }

        public HttpApiResponse()
        {
            Data = new List<UserApi>();
        }
    }
}
