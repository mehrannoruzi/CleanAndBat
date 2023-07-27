namespace CleanAndBat.Persistence.UnitOfWork;

public partial class AppUnitOfWork
{
	#region Auth
	public GenericRepo<Otp> OtpRepo => (GenericRepo<Otp>)_serviceProvider.GetService<IGenericRepo<Otp>>();
	#endregion

	#region Base
	public GenericRepo<User> UserRepo => (GenericRepo<User>)_serviceProvider.GetService<IGenericRepo<User>>();
	#endregion
}