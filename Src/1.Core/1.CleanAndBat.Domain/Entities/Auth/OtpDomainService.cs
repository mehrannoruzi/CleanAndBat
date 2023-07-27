namespace CleanAndBat.Domain.Entities;

public partial class Otp
{
    public static Otp GetNewInstance(int pinCodeLengh)
    {
        var otp = new Otp
        {
            IsUsed = false,
            UsedTime = null,
            PinCode = Helper.GetRandomInt(pinCodeLengh).ToString()
        };

        return otp;
    }
}