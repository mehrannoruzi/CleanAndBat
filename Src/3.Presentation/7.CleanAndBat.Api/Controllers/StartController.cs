namespace CleanAndBat.Api.Controllers;

public class StartController : Controller
{
	private readonly IWebHostEnvironment _webHost;
	private readonly IServiceProvider _serviceProvider;

	public StartController(IWebHostEnvironment webHost, IServiceProvider serviceProvider)
	{
		_webHost = webHost;
		_serviceProvider = serviceProvider;
	}


	[HttpGet]
	[Route("")]
	[Route("[Controller]/[Action]")]
	public IActionResult Index()
		=> Ok($"Welcome to {_webHost.ApplicationName} in {_webHost.EnvironmentName} Mode ... DateTime: {DateTime.Now.ToPersianDate()} {DateTime.Now.ToFullTime()}");

	[HttpGet]
	[Route("[Controller]/[Action]")]
	public async Task<IActionResult> Index2()
	{
		//var cardHash = "6219861053740873".ToBehPardakhtCardEncryption();
		//C124FE079423C004E97DB0668F643B32

		//var nationalCodeHash = "5560474657".ToBehPardakhtNationalCodeEncryption();
		//C124FE079423C004831A4D7C3C73B553

		//var behPardakhtRequestDto = new BehPardakhtRequestDto
		//{
		//    Number = 5050522,
		//    Amount = 50000,
		//    Username = "betya523",
		//    Password = "20440400",
		//    ReturnUrl = "api2.hillapay.ir"
		//};
		//var payResult = await _behPardakhtAdapter.RequestForPay(behPardakhtRequestDto);

		await Task.CompletedTask;
		return Json(Response<object>.Success(true, "Success"));
	}
}