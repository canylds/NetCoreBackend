using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security;
using Core.Extensions;

namespace Business.BusinessAspects;

public class SecuredOperation : MethodInterception
{
    private string[]? _roles;
    private IHttpContextAccessor _httpContextAccessor;

    public SecuredOperation(string roles = null)
    {
        _roles = roles?.Split(',');
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var userId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new SecurityException(Messages.AuthorizationDenied);

        if (_roles != null && _roles.Any())
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new SecurityException(Messages.AuthorizationDenied);
        }
    }
}
