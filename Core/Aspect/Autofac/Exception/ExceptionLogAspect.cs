using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspect.Autofac.Exception;

public class ExceptionLogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExceptionLogAspect()
    {
        _loggerServiceBase = ServiceTool.ServiceProvider.GetService<LoggerServiceBase>()!;
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;
    }

    protected override void OnException(IInvocation invocation, System.Exception e)
    {
        var logDetailWithException = GetLogDetail(invocation);

        logDetailWithException.ExceptionMessage = e is AggregateException
            ? string.Join(Environment.NewLine, (e as AggregateException).InnerExceptions.Select(x => x.Message))
            : e.Message;
        _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation)
    {
        var logParameters = invocation.Arguments.Select((t, i) => new LogParameter
        {
            Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
            Value = t,
            Type = t.GetType().Name
        }).ToList();

        var logDetailWithException = new LogDetailWithException
        {
            MethodName = invocation.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?"
        };
        return logDetailWithException;
    }
}
