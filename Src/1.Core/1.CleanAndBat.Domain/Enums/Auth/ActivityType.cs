namespace CleanAndBat.Domain.Enums;

public enum ActivityType : byte
{
	[Description("Register")]
	Register = 1,

	[Description("Login")]
	Login = 2,

	[Description("LogOut")]
	LogOut = 3,

	[Description("RefreshToken")]
	RefreshToken = 4,

	[Description("RecoverPassword")]
	RecoverPassword = 5,

	[Description("UpdatePassword")]
	UpdatePassword = 6,

	[Description("UpdateProfile")]
	UpdateProfile = 7,

}