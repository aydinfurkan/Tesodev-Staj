using System;
using System.Net;

namespace FileApi.Http.Model
{
    public class HttpException : Exception
    {
        public override string Message { get; }
        public int Code { get; }
        public HttpStatusCode HttpStatusCode { get; }

        protected HttpException(string message, int code, HttpStatusCode httpStatusCode)
        {
            Message = message;
            Code = code;
            HttpStatusCode = httpStatusCode;
        }

        public object ConvertDto()
        {
            return new
            {
                Message,
                Code
            };
        }
    }
}