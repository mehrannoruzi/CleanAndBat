namespace CleanAndBat.Dto;

public class SendOtpDto
{
	public int UserId { get; set; }
    public int PinCodeLenth { get; set; }
	public long MobileNumber { get; set; }
    public ActivityType ActivityType { get; set; }
}