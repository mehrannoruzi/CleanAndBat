namespace CleanAndBat.Dto;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
	public RegisterUserDtoValidator()
	{
		RuleFor(x => x.MobileNumber).LessThanOrEqualTo(0).WithMessage(DtoMessages.InvalidMobileNumber);
		RuleFor(x => x.FirstName).Null().Length(3, 25).WithMessage(DtoMessages.InvalidFirstName);
		RuleFor(x => x.LastName).Null().Length(5, 30).WithMessage(DtoMessages.InvalidLastName);
		RuleFor(x => x.Password).Null().Length(8).WithMessage(DtoMessages.InvalidPasswordLenght);
	}
}