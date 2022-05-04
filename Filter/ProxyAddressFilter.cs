using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace watchify.Filter;

public class ProxyAddressFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues headerValues);
        var header = headerValues.FirstOrDefault();
        if (!String.IsNullOrEmpty(header))
        {
            context.HttpContext.Connection.RemoteIpAddress = IPAddress.Parse(header);
        }
    }
}