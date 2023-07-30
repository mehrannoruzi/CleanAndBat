namespace CleanAndBat.Dto;

public record RegisterUserDto
{
	public long MobileNumber { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required string Password { get; set; }
	public string? Email { get; set; }
}