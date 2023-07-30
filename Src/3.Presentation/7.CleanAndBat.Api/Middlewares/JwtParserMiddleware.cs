namespace CleanAndBat.Api;

public class JwtParserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public JwtParserMiddleware(RequestDelegate next, IJwtService jwtService,
        IOptions<JwtSettings> jwtSettings)
    {
        _next = next;
        _jwtService = jwtService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        try
        {
            if (token != null)
            {
                var userClaims = _jwtService.GetClaimsPrincipal(token, _jwtSettings);
                if (userClaims != null) context.User = userClaims;
            }
            else
            {
                if (context.User.Claims.Any())
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/Json";
                    var bytes = Encoding.ASCII.GetBytes(new { isSuccessful = false, message = "UnAuthorized Access To Api !. Token Not Sent.", resultCode = 401 }.SerializeToJson());
                    await context.Response.Body.WriteAsync(bytes);
                }
            }

            await _next(context);
        }
        catch (Exception e)
        {
            byte[] bytes;
            if (e.Message.Contains("Lifetime validation failed"))
            {
                #region Expired Token
                //var validationTime = _jwtService.GetTokenExpireTime(token, _jwtSettings);
                bytes = Encoding.UTF8.GetBytes(new
                {
                    resultCode = 1001,
                    isSuccessful = false,
                    message = "با توجه به عدم فعالیت، لطفا مجددا وارد سامانه شوید ",
                }.SerializeToJson());
                #endregion
            }
            else
            {
                #region Another Exception
                bytes = Encoding.UTF8.GetBytes(new
                {
                    resultCode = 1002,
                    isSuccessful = false,
                    message = "عملیات مورد نظر با خطا رو به رو شده است، لطفا مجددا تلاش کنید." +
                    Environment.NewLine + e.Message
                }.SerializeToJson());
                #endregion
            }

			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/Json";
            await context.Response.Body.WriteAsync(bytes);
        }
    }
}