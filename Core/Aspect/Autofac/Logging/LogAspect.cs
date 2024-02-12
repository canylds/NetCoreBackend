﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspect.Autofac.Logging;

public class LogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogAspect()
    {
        _loggerServiceBase = ServiceTool.ServiceProvider.GetService<LoggerServiceBase>()!;
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _loggerServiceBase?.Info(GetLogDetail(invocation));
    }

    private string GetLogDetail(IInvocation invocation)
    {
        var logParameters = new List<LogParameter>();
        for (var i = 0; i < invocation.Arguments.Length; i++)
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Value = invocation.Arguments[i],
                Type = invocation.Arguments[i].GetType().Name,
            });
        }

        var logDetail = new LogDetail
        {
            MethodName = invocation.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?"

        };
        return JsonConvert.SerializeObject(logDetail);
    }
}
