namespace CleanAndBat.Interface.BussinessInterface;

public interface IUserService : IScopedInjection
{
	Task<Response<int>> Register(RegisterUserDto model, HttpContext httpContext, CancellationToken cancellationToken = default);
	Task<IResponse<object?>> GetProfile(int userId, CancellationToken cancellationToken = default);
	Task<IResponse<object?>> Filter(FilterUserDto filterUserDto, PagingParameter pagingParameter, CancellationToken cancellationToken = default);
}