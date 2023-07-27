namespace CleanAndBat.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger;

	public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private async Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		var requestBody = await context.Request.ReadRequestBody();
		var message = $"Url: {context.Request.Path}, QueryString: {context.Request.QueryString.Value}, RequestBody: {requestBody}";
		_logger.LogError(exception: ex, message: message);

		var response = new
		{
			isSuccess = false,
			resultCode = (int)HttpStatusCode.BadRequest,
			message = "عملیات مورد نظر در سرور با خطا مواجه شده است، لطفا مجددا اقدام نمایید."
		};
		context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
		context.Response.ContentType = "application/Json";
		var responseBody = Encoding.UTF8.GetBytes(response.SerializeToJson());
		await context.Response.Body.WriteAsync(responseBody);
	}
}