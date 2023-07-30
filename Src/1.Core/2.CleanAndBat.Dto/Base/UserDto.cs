namespace CleanAndBat.Dto;

[SwaggerExclude]
public record UserDto
{
	[JsonIgnore]
	public int UserId { get; set; }
	public long MobileNumber { get; set; }
	[JsonIgnore]
	public bool IsActive { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }

	[JsonIgnore]
	public string? Password { get; set; }

	[JsonIgnore]
	public string? PasswordSalt { get; set; }


	public override int GetHashCode()
		=> MobileNumber.GetHashCode();

	//public override bool Equals(object? obj)
	//{
	//	if (obj == null) return false;
	//	return obj is UserDto model && model.MobileNumber == MobileNumber;
	//}
}