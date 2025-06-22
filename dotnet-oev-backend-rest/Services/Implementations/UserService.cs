using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;


    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IReadOnlyList<UserResponseDTO>> FindAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.FindAllAsync();
        return _mapper.Map<IReadOnlyList<UserResponseDTO>>(users);
    }

    public async Task<UserResponseDTO?> FindUserByIdAsync(long id)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(id);
        if (user == null) throw new NotFoundException($"User with id {id} not found");

        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO?> FindUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.UserRepository.FindUserByEmailAsync(email);
        if (user == null) throw new NotFoundException($"User with email {email} not found");

        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO> UpdateUserByIdAsync(long id, UpdateUserRequestDTO updateUserRequestDTO)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(id);
        if (user == null) throw new NotFoundException($"User with id {id} not found");

        // Map the update request to the user entity
        _mapper.Map(updateUserRequestDTO, user);

        // Save changes to the database.
        // Not necessary to call _unitOfWork.UserRepository.Update(user) because the entity is already tracked by EF Core.
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<bool> DeleteUserByIdAsync(long id)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(id);
        if (user == null) throw new NotFoundException($"User with id {id} not found");

        // Delete the user
        _unitOfWork.UserRepository.Delete(user);

        // Save changes to the database
        await _unitOfWork.CompleteAsync();

        return true;
    }
}