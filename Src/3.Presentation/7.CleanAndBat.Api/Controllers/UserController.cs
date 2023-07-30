namespace CleanAndBat.Api.Controllers;

[AuthorizeFilter]
[Route("[controller]/[action]")]
public class UserController : Controller
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}


	[HttpPost]
	public async Task<JsonResult> Register([FromBody] RegisterUserDto registerUserDto, CancellationToken cancellationToken)
		=> Json(await _userService.Register(registerUserDto, HttpContext, cancellationToken));

	[HttpGet]
	public async Task<JsonResult> GetProfile([FromHeader] UserDto userDto, CancellationToken cancellationToken)
		=> Json(await _userService.GetProfile(userDto.UserId, cancellationToken));

	[HttpGet]
	public async Task<JsonResult> Filter([FromQuery] FilterUserDto filterUserDto, PagingParameter pagingParameter, CancellationToken cancellationToken)
		=> Json(await _userService.Filter(filterUserDto, pagingParameter, cancellationToken));

}