namespace CleanAndBat.Interface.BussinessInterface;

public interface IOtpService : IScopedInjection
{
	Task<IResponse<int>> SendCode(SendOtpDto sendOtpDto, CancellationToken cancellationToken = default);
	Task<IResponse<int>> SendCodeViaEmail(int userId, string mail, ActivityType activityType, int pinLenth = 6);
	Task<IResponse<bool>> ReSendCode(int otpId, CancellationToken cancellationToken = default);
	Task<IResponse<bool>> ReSendCodeViaEmail(int otpId, CancellationToken cancellationToken = default);
	Task<IResponse<object>> VerifyCode(int otpId, string pinCode, CancellationToken cancellationToken = default);
}