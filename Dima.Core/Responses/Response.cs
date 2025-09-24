using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dima.Core.Responses
{
    public class Response<TData>
    {
        private readonly int _code = Configurations.DefaultStatusCode;

        [JsonConstructor]
        public Response()
        {
            _code = Configurations.DefaultStatusCode;
        }

        public Response(TData? data,int code = 200, string? message = null)
        {
            Data = data;
            _code = Configurations.DefaultStatusCode;
            Message = message;
            
        }

        public TData? Data { get; set; }
        public string? Message { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;
        

    }
}
