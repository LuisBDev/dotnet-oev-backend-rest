using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public async Task<AuthUserResponseDTO> LoginAsync(UserLoginRequestDTO userLoginRequestDto)
    {
        var email = userLoginRequestDto.Email;
        var password = userLoginRequestDto.Password;

        var user = await _unitOfWork.UserRepository.FindUserByEmailAsync(email);

        if (user == null) throw new Exception($"User with email: {email} does not exist");

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new Exception("Invalid password");

        return _mapper.Map<AuthUserResponseDTO>(user);
    }

    public async Task<UserResponseDTO> RegisterAsync(UserRegisterRequestDTO userRegisterRequestDto)
    {
        var email = userRegisterRequestDto.Email;
        var user = await ValidateUserByEmail(email);

        var userEntity = _mapper.Map<User>(userRegisterRequestDto);

        userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterRequestDto.Password);

        await _unitOfWork.UserRepository.AddAsync(userEntity);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<UserResponseDTO>(userEntity);
    }


    private async Task<User> ValidateUserByEmail(string email)
    {
        var user = await _unitOfWork.UserRepository.FindUserByEmailAsync(email);

        if (user != null) throw new Exception($"User with email: {email} exists");

        return user;
    }
}