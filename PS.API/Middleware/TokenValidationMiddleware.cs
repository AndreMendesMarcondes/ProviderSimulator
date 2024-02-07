using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PS.API.Middleware
{
    public class TokenValidationMiddleware
    {
        public class ValidateApiTokenAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var token = context.HttpContext.Request.Headers["ApiToken"].FirstOrDefault();

                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedResult(); 
                    return;
                }

                try
                {
                    var tokenDecodedBytes = Convert.FromBase64String(token);
                    var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);

                    if (!tokenDecoded.Contains("Biludinhos"))
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
                catch
                {
                    context.Result = new UnauthorizedResult(); 
                    return;
                }

                base.OnActionExecuting(context);
            }
        }
    }
}
