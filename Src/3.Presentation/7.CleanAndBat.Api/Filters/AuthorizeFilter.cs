namespace CleanAndBat.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeFilter : ActionFilterAttribute, IAsyncActionFilter
{
	private static UserDto ConvertUserClaimsToUserDto(IEnumerable<Claim> userClaims)
	{
		var user = new UserDto();
		foreach (var claim in userClaims)
		{
			switch (claim.Type)
			{
				case "UserId":
					user.UserId = int.Parse(claim.Value);
					break;
				case "MobileNumber":
					user.MobileNumber = long.Parse(claim.Value);
					break;
				case "IsActive":
					user.IsActive = bool.Parse(claim.Value);
					break;
				case "FirstName":
					user.FirstName = claim.Value;
					break;
				case "LastName":
					user.LastName = claim.Value;
					break;
			}
		}

		return user;
	}

	public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
	{
		try
		{
			var ActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor ?? throw new Exception("filterContext.ActionDescriptor in AuthorizeFilter is NULL.");
			bool skipAuthorize = ActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
				.Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
			if (!skipAuthorize)
			{
				var ip = ClientInfo.GetIP(filterContext.HttpContext);
				if (filterContext.HttpContext.User.Claims.Any())
				{
					#region Existing User Claims
					var userModel = ConvertUserClaimsToUserDto(filterContext.HttpContext.User.Claims);
					//var _cacheProvider = filterContext.HttpContext.RequestServices.GetService(typeof(IMemoryCacheProvider)) as IMemoryCacheProvider ?? throw new Exception("cacheProvider in AuthorizeFilter is NULL.");
					//var userMenu = (IEnumerable<MenuModel>)(_cacheProvider.Get(GlobalVariables.CacheSettings.MenuKey(userModel.UserId)));
					//if (userMenu.IsNull())
					//{
					//	var _userService = filterContext.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService ?? throw new Exception("userService in AuthorizeFilter is NULL.");
					//	userMenu = await _userService.GetAvailableMenus(userModel.UserId);
					//}

					//if (userMenu.IsNull())
					//{
					//	#region User Dosent Access To Path
					//	FileLoger.Info($"Access Denied. User Does Not Any Menu." + Environment.NewLine +
					//		$"IP: {ip}" + Environment.NewLine +
					//		$"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
					//		$"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

					//	filterContext.HttpContext.Response.StatusCode = 403;
					//	filterContext.Result = new JsonResult(new Response<object>
					//	{
					//		ResultCode = 403,
					//		IsSuccess = false,
					//		Message = "Access Denied. User Does Not Any Menu.",
					//	});
					//	#endregion
					//}

					//var allMenu = userMenu.GetAllMenu();

					////if (!allMenu.Any(x => x.Path.Contains(filterContext.HttpContext.Request.Path.Value)))
					////{
					////    #region User Dosent Access To Path
					////    FileLoger.Info($"Access Denied. User Does Not Access To Path." + Environment.NewLine +
					////        $"IP: {ip}" + Environment.NewLine +
					////        $"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
					////        $"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

					////    filterContext.HttpContext.Response.StatusCode = 403;
					////    filterContext.Result = new JsonResult(new Response<object>
					////    {
					////        ResultCode = 403,
					////        IsSuccessful = false,
					////        Message = "Access Denied. User Does Not Access To Path.",
					////    });
					////    #endregion
					////}

					//if (userModel.UserId != 0)
					//{
						if (filterContext.ActionArguments.ContainsKey(nameof(UserDto)))
							filterContext.ActionArguments[nameof(UserDto)] = userModel;
					//}
					//else
					//{
					//	FileLoger.Info($"Invalid Token Claims Data To Access Api !" + Environment.NewLine +
					//		$"IP: {ip}" + Environment.NewLine +
					//		$"UserId: {userModel.UserId}" + Environment.NewLine +
					//		$"URL:{filterContext.HttpContext.Request.Path.Value}" + Environment.NewLine +
					//		$"UrlReferer:{filterContext.HttpContext.Request.GetTypedHeaders().Referer}");

					//	filterContext.HttpContext.Response.StatusCode = 401;
					//	filterContext.Result = new JsonResult(new Response<object>
					//	{
					//		ResultCode = 401,
					//		IsSuccess = false,
					//		Message = "UnAuthorized Access. Invalid Token Claims Data To Access Api !"
					//	});
					//}
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