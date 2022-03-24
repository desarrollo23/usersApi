using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace User.Common.Response
{
    public class EntityResponse
    {
        public static EntityResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new EntityResponse(statusCode, result, errorMessage);
        }

        public int StatusCode { get; set; }
        public string RequestId { get; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        protected EntityResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
