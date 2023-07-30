namespace CleanAndBat.Dto;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
	public RegisterUserDtoValidator()
	{
		RuleFor(x => x.MobileNumber).GreaterThan(0).WithMessage(DtoMessages.InvalidMobileNumber);
		RuleFor(x => x.FirstName).NotNull().Length(3, 25).WithMessage(DtoMessages.InvalidFirstName);
		RuleFor(x => x.LastName).NotNull().Length(5, 30).WithMessage(DtoMessages.InvalidLastName);
		RuleFor(x => x.Password).NotNull().Length(8).WithMessage(DtoMessages.InvalidPasswordLenght);
	}
}