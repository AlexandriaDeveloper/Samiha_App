using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{

    public class ApiExceptions : ApiResponse
    {
        public string Details { get; }
        public ApiExceptions(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            this.Details = details;
        }
    }
}