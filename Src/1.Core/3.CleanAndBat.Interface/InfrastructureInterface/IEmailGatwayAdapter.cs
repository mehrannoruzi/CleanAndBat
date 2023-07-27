namespace CleanAndBat.Interface.InfrastructureInterface;

public interface IEmailGatwayAdapter : IScopedInjection
{
	IResponse<bool> Send(IServiceProvider serviceProvider, string receiver, string subject, string text);
}