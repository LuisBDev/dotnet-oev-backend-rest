using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Implementations;
using dotnet_oev_backend_rest.Tests.Helpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace dotnet_oev_backend_rest.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = MapperHelper.CreateMapper();
        _userService = new UserService(_unitOfWorkMock.Object, _mapper);
    }

    [Fact]
    public async Task FindAllUsersAsync_ReturnsMappedUsers()
    {
        // Arrange
        var users = UserTestData.CreateUserList();
        _unitOfWorkMock.Setup(u => u.UserRepository.FindAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.FindAllUsersAsync();

        // Assert
        result.Should().HaveCount(users.Count);
        result[0].Email.Should().Be(users[0].Email);
    }

    [Fact]
    public async Task FindUserByIdAsync_UserExists_ReturnsUser()
    {
        // Arrange
        var user = UserTestData.CreateValidUser();
        _unitOfWorkMock.Setup(u => u.UserRepository.FindByIdAsync(user.Id)).ReturnsAsync(user);

        // Act
        var result = await _userService.FindUserByIdAsync(user.Id);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task FindUserByIdAsync_UserNotFound_ThrowsNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.UserRepository.FindByIdAsync(It.IsAny<long>())).ReturnsAsync((User)null);

        // Act
        var act = async () => await _userService.FindUserByIdAsync(99);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task UpdateUserByIdAsync_UserExists_UpdatesAndReturnsUser()
    {
        // Arrange
        var user = UserTestData.CreateValidUser();
        var updateDto = UserTestData.CreateValidUpdateUserRequest();
        _unitOfWorkMock.Setup(u => u.UserRepository.FindByIdAsync(user.Id)).ReturnsAsync(user);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _userService.UpdateUserByIdAsync(user.Id, updateDto);

        // Assert
        result.Name.Should().Be(updateDto.Name);
        result.Email.Should().Be(updateDto.Email);
    }

    [Fact]
    public async Task DeleteUserByIdAsync_UserExists_DeletesAndReturnsTrue()
    {
        // Arrange
        var user = UserTestData.CreateValidUser();
        _unitOfWorkMock.Setup(u => u.UserRepository.FindByIdAsync(user.Id)).ReturnsAsync(user);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _userService.DeleteUserByIdAsync(user.Id);

        // Assert
        result.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.UserRepository.Delete(user), Times.Once);
    }

    [Fact]
    public async Task DeleteUserByIdAsync_UserNotFound_ThrowsNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.UserRepository.FindByIdAsync(It.IsAny<long>())).ReturnsAsync((User)null);

        // Act
        var act = async () => await _userService.DeleteUserByIdAsync(99);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
