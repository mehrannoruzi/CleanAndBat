namespace CleanAndBat.Infrastructure.EmailGateway;

public class NajvaEmailAdapter : IEmailGatwayAdapter
{
	public IResponse<bool> Send(IServiceProvider serviceProvider, string receiver, string subject, string text)
	{
		throw new NotImplementedException();
	}
}