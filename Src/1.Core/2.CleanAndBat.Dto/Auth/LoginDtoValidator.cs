namespace CleanAndBat.Dto;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
	public LoginDtoValidator()
	{
		RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage(DtoMessages.InvalidUsernameOrPassword);
		RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(DtoMessages.InvalidUsernameOrPassword);
	}
}