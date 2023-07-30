namespace CleanAndBat.Dto;

public class LoginDto
{
	[DefaultValue("9301919109")]
	public long Username { get; set; }

	[DefaultValue("12345678")]
	public string Password { get; set; }
}