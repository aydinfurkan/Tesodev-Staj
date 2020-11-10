using System.Net;
using FileApi.Http.Model;

namespace FileApi.Http
{
    public class HttpBadRequest : HttpException
    {
        public HttpBadRequest() : base("Syntax error!", 100, HttpStatusCode.BadRequest)
        {
        }
    }
    
    public class HttpUnauthorized : HttpException
    {
        public HttpUnauthorized() : base("Authorize first please!", 101, HttpStatusCode.Unauthorized)
        {
        }
    }
    
    public class HttpForbidden : HttpException
    {
        public HttpForbidden() : base("You have not access here!", 102, HttpStatusCode.Forbidden)
        {
        }
    }
    
    public class HttpNotFound : HttpException
    {
        public HttpNotFound(string e) : base(e + " not found.", 103, HttpStatusCode.NotFound)
        {
        }
    }
    
    public class HttpMethodNotAllowed : HttpException
    {
        public HttpMethodNotAllowed() : base("Change method!", 104, HttpStatusCode.MethodNotAllowed)
        {
        }
    }
}