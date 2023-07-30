namespace CleanAndBat.ApplicationService.Implements;

public class UserService : IUserService
{
	private readonly AppUnitOfWork _appUow;
	private readonly Lazy<IOtpService> _otpService;
	private readonly IValidator<RegisterUserDto> _validator;
	//private readonly Lazy<ISmsGatwayAdapter> _smsGatwayAdapter;

	public UserService(AppUnitOfWork appUow, IValidator<RegisterUserDto> validator,
		Lazy<IOtpService> otpService)
	{
		_appUow = appUow;
		_validator = validator;
		_otpService = otpService;
		//_smsGatwayAdapter = smsGatwayAdapter;
	}


	public async Task<Response<int>> Register(RegisterUserDto registerUserDto, HttpContext httpContext, CancellationToken cancellationToken = default)
	{
		#region Validate Request
		var validationResult = await _validator.ValidateAsync(registerUserDto, cancellationToken);
		if (!validationResult.IsValid)
			return Response<int>.Error(validationResult.GetValidationErrors());
		#endregion

		var dbTrans = await _appUow.Database.BeginTransactionAsync(cancellationToken);
		try
		{
			#region Add User
			var passwordSalt = Randomizer.GetUniqueKey(8);
			var password = HashGenerator.Hash(registerUserDto.Password, passwordSalt);
			var user = User.GetNewInstance(
				registerUserDto.FirstName,
				registerUserDto.LastName,
				password,
				passwordSalt);

			user.MobileNumber = registerUserDto.MobileNumber;
			user.Email = registerUserDto.Email;
			_appUow.UserRepo.Add(user);
			var addUserResult = await _appUow.BatSaveChangesAsync(cancellationToken);
			if (!addUserResult.IsSuccess && addUserResult.ResultType == SaveChangeResultType.DuplicateIndexKeyException)
				return new Response<int>(ServiceMessages.RegisteredUserExist);
			#endregion

			#region Add Role
			//var userInRole = new UserInRole
			//{
			//	RoleId = 1,
			//	UserId = user.UserId,
			//};
			//_appUow.UserInRoleRepo.Add(userInRole);
			#endregion

			#region Insert Activity Log
			//var activity = new UserActivity
			//{
			//	UserId = user.UserId,
			//	Type = ActivityType.Register,
			//	DeviceLog = httpContext.GetDeviceLog().SerializeToJson()
			//};
			//_appUow.UserActivityRepo.Add(activity);
			#endregion

			#region Send Verify Code
			var sendOtpDto = new SendOtpDto
			{
				PinCodeLenth = 4,
				UserId = user.UserId,
				ActivityType = ActivityType.Register,
				MobileNumber = registerUserDto.MobileNumber,
			};
			var SendRegisterCodeResult = await _otpService.Value.SendCode(sendOtpDto, cancellationToken);
			if (SendRegisterCodeResult.IsSuccess is false)
			{
				await dbTrans.RollbackAsync(cancellationToken);
				return Response<int>.Error(ServiceMessages.Error);
			}

			//await _smsGatwayAdapter.Value.SendAsync(
			//	receiver: $"0{user.MobileNumber}",
			//	text: ServiceMessages.RegisterNewUser);
			#endregion

			await dbTrans.CommitAsync(cancellationToken);
			return Response<int>.Success(SendRegisterCodeResult.Result, ServiceMessages.Success);
		}
		catch
		{
			await dbTrans.RollbackAsync(cancellationToken);
			throw;
		}
	}

	public async Task<IResponse<object?>> GetProfile(int userId, CancellationToken cancellationToken = default)
	{
		var userProfile = await _appUow.UserRepo
			.AsNoTracking()
			.Where(x => x.UserId == userId)
			.Select(x => new
			{
				#region Init Props
				x.FirstName,
				x.LastName,
				x.IsActive,
				x.MobileNumber,
				x.Email,
				#endregion
			})
			.FirstOrDefaultAsync(cancellationToken);

		return Response<object?>.Success(userProfile, ServiceMessages.Success);
	}

	public async Task<IResponse<object?>> Filter(FilterUserDto filterUserDto, PagingParameter pagingParameter, CancellationToken cancellationToken = default)
	{
		var userList = await _appUow.UserRepo
			.AsNoTracking()
			.Where(x => (filterUserDto.MobileNumber == 0 || x.MobileNumber == filterUserDto.MobileNumber) ||
						(filterUserDto.FirstName == null || x.FirstName.Contains(filterUserDto.FirstName)) ||
						(filterUserDto.LastName == null || x.LastName.Contains(filterUserDto.LastName)))
			.Select(x => new
			{
				#region Init Props
				x.FirstName,
				x.LastName,
				x.IsActive,
				x.MobileNumber,
				x.Email,
				#endregion
			})
			.ToPagingListDetailsAsync(pagingParameter, cancellationToken);

		return Response<object?>.Success(userList, ServiceMessages.Success);
	}
}