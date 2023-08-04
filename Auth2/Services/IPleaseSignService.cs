using Auth2.Models;

namespace Auth2.Services
{
	public interface IPleaseSignService
	{
		/// <summary>
		/// Method to check if use authenticated.
		/// </summary>
		/// <param name="userId">Id of user.</param>
		/// <returns>AuthResponse</returns>
		Task<AuthResponseModel> AuthenticatedAsync(Guid userId);
		
		/// <summary>
		/// Method to retrieve token.
		/// </summary>
		/// <param name="userId">Id of user.</param>
		/// <param name="tokenRequestModel">Model request</param>
		/// <returns>AuthResponse model.</returns>
		Task<AuthResponseModel> RequestTokenAsync(Guid userId, TokenRequestModel tokenRequestModel);
		
		/// <summary>
		/// Method to retrieve token by user id.
		/// </summary>
		/// <param name="userId">Id of user.</param>
		/// <returns>AuthResponse</returns>
		Task<UserModel> RefreshTokenAsync(Guid userId);
	}
}
