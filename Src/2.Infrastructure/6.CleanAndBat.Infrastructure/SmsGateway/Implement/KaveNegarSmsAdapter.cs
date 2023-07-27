namespace CleanAndBat.Infrastructure.SmsGateway;

public class KaveNegarSmsAdapter : ISmsGatwayAdapter
{
	public Task<IResponse<long>> GetCreditAsync()
	{
		throw new NotImplementedException();
	}

	public Task<IResponse<bool>> SendAsync(string receiver, string text, bool isFlash = false)
	{
		throw new NotImplementedException();
	}

	public Task<IResponse<List<bool>>> SendMultipleAsync(List<string> receiver, string text, bool isFlash = false)
	{
		throw new NotImplementedException();
	}

	public Task<IResponse<List<bool>>> SendMultipleAsync(List<string> receiver, List<string> text, bool isFlash = false)
	{
		throw new NotImplementedException();
	}
}