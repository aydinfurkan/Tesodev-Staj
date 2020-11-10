using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gateway.Helpers;
using Gateway.Http;
using Gateway.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gateway.Attribute
{
    public class MyUserAuthorize : System.Attribute, IAsyncActionFilter
    {
        private Roles[] _roles;

        public MyUserAuthorize()
        {
            _roles = (Roles[])Enum.GetValues(typeof(Roles));
        }
        public MyUserAuthorize(params Roles[] roles)
        {
            _roles = roles;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var jwt = context.HttpContext.Session.GetString("jwt");
            
            CheckLogin(jwt);
            
            var claims = JwtHelper.GetClaims(jwt);
            var roleStr = claims.First(x => x.Type == ClaimTypes.Role).Value;
            
            CheckAllowedRoles(roleStr);
            

            return next.Invoke();
        }
        
        private void CheckLogin(string jwt)
        {
            if(jwt == null) throw new HttpUnauthorized();
        }
        
        private void CheckAllowedRoles(string roleStr)
        {
            Enum.TryParse(roleStr, out Roles role);
            
            if(!_roles.ToList().Contains(role)) throw new HttpForbidden();
        }
    }
}