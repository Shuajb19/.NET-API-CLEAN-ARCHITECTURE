
using Application.Dto;

namespace Application.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO);

        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}
