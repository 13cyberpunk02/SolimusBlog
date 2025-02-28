using SolimusBlog.API.Endpoints;

namespace SolimusBlog.API.Extensions;

public static class EndpointsExtension
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapIdentityEndpoints();
        return endpoints;
    }
}