namespace CleanAndBat.Interface.InfrastructureInterface;

public interface ISmsGatwayAdapter : IScopedInjection
{
	Task<IResponse<long>> GetCreditAsync();
	Task<IResponse<bool>> SendAsync(string receiver, string text, bool isFlash = false);
	Task<IResponse<List<bool>>> SendMultipleAsync(List<string> receiver, string text, bool isFlash = false);
	Task<IResponse<List<bool>>> SendMultipleAsync(List<string> receiver, List<string> text, bool isFlash = false);
}