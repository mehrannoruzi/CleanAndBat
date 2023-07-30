namespace CleanAndBat.Dto;

public record FilterUserDto
{
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public long MobileNumber { get; set; }
}