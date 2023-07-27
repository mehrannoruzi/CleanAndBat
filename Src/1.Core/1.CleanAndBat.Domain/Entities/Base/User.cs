namespace CleanAndBat.Domain.Entities;

public partial class User : IEntity, IInsertDateProperty, ISoftDeleteProperty
{
	public int UserId { get; set; }

	public long MobileNumber { get; set; }

	public bool IsActive { set; get; }

	public bool IsDeleted { set; get; }

	public DateTime InsertDateMi { get; set; }

	[JsonIgnore]
	public required string PasswordSalt { get; set; }

	[JsonIgnore]
	public required string Password { get; set; }

	public required string FirstName { get; set; }

	public required string LastName { get; set; }

	public string? Email { get; set; }


	[JsonIgnore]
	public List<Otp>? Otps { get; set; }
}