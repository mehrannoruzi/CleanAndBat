namespace CleanAndBat.Dto;

public class SendOtpDtoValidator : AbstractValidator<SendOtpDto>
{
	public SendOtpDtoValidator()
	{
		RuleFor(x => x.MobileNumber).LessThanOrEqualTo(0).WithMessage(DtoMessages.InvalidMobileNumber);
		RuleFor(x => x.UserId).LessThanOrEqualTo(0).WithMessage(DtoMessages.InvalidUserId);
		RuleFor(x => x.PinCodeLenth).LessThan(4).WithMessage(DtoMessages.PinCodeLenth);
	}
}