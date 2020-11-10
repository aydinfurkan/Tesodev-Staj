using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gateway.Attribute
{
    public class MyFileAuthorize : System.Attribute, IAsyncActionFilter
    {
        public MyFileAuthorize()
        {
        }
        
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestPath = context.HttpContext.Request.Path.ToString().Split('/');

            var fileId = requestPath[^1];
            
            
            
            return next.Invoke();
        }
    }
}