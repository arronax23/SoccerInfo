using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SoccerInfo.API.Authorization;
public class ApiKeyAuthorizationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var exctractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Api key is missing");
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration.GetValue<string>(AuthConstants.ApiKeySectionName)!;

        if (!apiKey.Equals(exctractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Invalid api key");
        }
    }
}
