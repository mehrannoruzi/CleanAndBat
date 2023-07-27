namespace CleanAndBat.ApplicationService.Implements;

public class OtpService : IOtpService
{
	private readonly AppUnitOfWork _appUow;
	private readonly IValidator<SendOtpDto> _validator;

	public OtpService(AppUnitOfWork appUow, IValidator<SendOtpDto> validator)
	{
		_appUow = appUow;
		_validator = validator;
	}


	public async Task<IResponse<int>> SendCode(SendOtpDto sendOtpDto, CancellationToken cancellationToken = default)
	{
		var validationResult = await _validator.ValidateAsync(sendOtpDto, cancellationToken);
		if (!validationResult.IsValid)
			return Response<int>.Error(validationResult.GetValidationErrors());

		#region Add virifyCode
		var randomPinCode = Randomizer.GetRandomInteger(sendOtpDto.PinCodeLenth).ToString();
		var otpCode = new Otp
		{
			UserId = sendOtpDto.UserId,
			IsUsed = false,
			PinCode = randomPinCode,
			ActivityType = sendOtpDto.ActivityType,
			MobileNumber = sendOtpDto.MobileNumber,
			ExpirationTime = DateTime.Now.AddMinutes(4),
		};
		_appUow.OtpRepo.Add(otpCode);
		#endregion

		#region Add Notification
		//var notification = new Notification
		//{
		//	TryCount = 0,
		//	IsSent = false,
		//	UserId = userId,
		//	ActivityType = activityType,
		//	Type = NotificationType.Sms,
		//	Reciver = mobileNumber.ToString(),
		//	Text = ServiceMessages.SendVerificationMessage.Replace("@", $"{randomPinCode}")
		//};
		//_appUow.NotificationRepo.Add(notification);
		#endregion

		var sendCodeResult = await _appUow.BatSaveChangesAsync();
		if (sendCodeResult.IsSuccess) return Response<int>.Success(otpCode.OtpId, ServiceMessages.Success);
		return Response<int>.Error(ServiceMessages.Error);
	}

	public Task<IResponse<bool>> ReSendCode(int otpId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<IResponse<bool>> ReSendCodeViaEmail(int otpId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<IResponse<int>> SendCodeViaEmail(int userId, string mail, ActivityType activityType, int pinLenth = 6)
	{
		throw new NotImplementedException();
	}

	public Task<IResponse<object>> VerifyCode(int otpId, string pinCode, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}