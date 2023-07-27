namespace CleanAndBat.Domain.Entities;

public partial class User
{
	public static User GetNewInstance(string firsName, string lastName, string password, string passwordSalt)
	{
		var user = new User
		{
			FirstName = firsName,
			LastName = lastName,
			Password = password,
			PasswordSalt = passwordSalt,
			IsActive = true,
			IsDeleted = false
		};

		return user;
	}
}