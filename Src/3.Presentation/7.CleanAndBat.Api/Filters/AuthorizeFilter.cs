﻿namespace CleanAndBat.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeFilter : ActionFilterAttribute, IAsyncActionFilter
{
	public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
	{
		try
		{
			var ActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
			bool skipAuthorize = ActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
			if (!skipAuthorize)
			{
				var ip = ClientInfo.GetIP(filterContext.HttpContext);
				if (filterContext.HttpContext.User.Claims.Any())
				{
					#region Existing User Claims
					
					#endregion
				}
				else
				{
					#region Not Existing User Claims
					FileLoger.Info($"UnAuthorized Access To Api. Token Not Sent." + Environment.NewLine +
						$"IP: {ip}" + Environment.NewLine +
						$"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
						$"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

					filterContext.HttpContext.Response.StatusCode = 403;
					filterContext.Result = new JsonResult(new Response<object>
					{
						ResultCode = 403,
						IsSuccess = false,
						Message = "UnAuthorized Access To Api. Token Not Sent.",
					});
					#endregion
				}
			}

			await base.OnActionExecutionAsync(filterContext, next);
		}
		catch (Exception e)
		{
			FileLoger.Error(e);
			filterContext.HttpContext.Response.StatusCode = 500;
			filterContext.Result = new JsonResult(new Response<object>
			{
				ResultCode = 500,
				IsSuccess = false,
				Message = "Internall Error." + e.Message
			});

			await base.OnActionExecutionAsync(filterContext, next);
		}
	}
}