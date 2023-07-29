using CleanAndBat.Dto;
using CleanAndBat.Interface.BussinessInterface;

namespace CleanAndBat.Api.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost]
    public async Task<JsonResult> Register(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        => Json(await _userService.Register(registerUserDto, HttpContext, cancellationToken));


    [HttpGet]
    public async Task<JsonResult> GetProfile(int userId, CancellationToken cancellationToken)
        => Json(await _userService.GetProfile(userId, cancellationToken));


    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}