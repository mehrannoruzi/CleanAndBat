namespace CleanAndBat.Api.Controllers;

[AuthenticateFilter]
[Route("[controller]/[action]")]
public class AuthController : Controller
{
	private readonly IJwtService _jwtService;
	private readonly JwtSettings _jwtSettings;

	public AuthController(IJwtService jwtService, IOptions<JwtSettings> jwtSettings)
	{
		_jwtService = jwtService;
		_jwtSettings = jwtSettings.Value;
	}


	/// <remarks>
	/// Sample request:
	/// 
	///     {
	///         "Username": 9301919109,
	///         "Password": "12345678"
	///     }
	/// 
	/// </remarks>
	[HttpPost]
	public JsonResult Login([FromBody] LoginDto loginDto)
	{
		var userClaims = new List<Claim>
		{
			new Claim("UserId", "1"),
			new Claim("MobileNumber", "09301919109"),
			new Claim("IsActive", "true"),
			new Claim("FirstName", "Mehran"),
			new Claim("LastName", "Norouzi")
		};

		var createTokenResult = _jwtService.CreateToken(userClaims, _jwtSettings);

		return Json(createTokenResult);
	}
}