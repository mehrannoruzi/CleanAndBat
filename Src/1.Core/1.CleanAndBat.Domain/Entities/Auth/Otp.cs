namespace CleanAndBat.Domain.Entities;

public partial class Otp : IEntity, IInsertDateProperty
{
	public int OtpId { get; set; }

	public User? User { get; set; }
	public int UserId { get; set; }

	public long MobileNumber { get; set; }

	public ActivityType ActivityType { get; set; }

	public bool IsUsed { get; set; }

	public DateTime InsertDateMi { get; set; }

	public DateTime ExpirationTime { get; set; }

	public DateTime? UsedTime { get; set; }

	public required string PinCode { get; set; }
}