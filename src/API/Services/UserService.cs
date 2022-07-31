﻿using API.Domain;
using API.Mapping;
using API.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CreateAsync(User user)
    {
        var existingUser = await _userRepository.GetAsync(user.Id.Value);
        if(existingUser is not null) 
        {
            var message = $"A user with id {user.Id} already exists";
            throw new ValidationException(message, GenerateValidationError(message));
        }

        var userDto = user.ToUserDto();

        return await _userRepository.CreateAsync(userDto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var userDtos = await _userRepository.GetAllAsync();

        return userDtos.Select(x => x.ToUser());
    }

    public async Task<User?> GetAsync(Guid id)
    {
        var userDto = await _userRepository.GetAsync(id);

        return userDto?.ToUser();
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var userDto = user.ToUserDto();

        return await _userRepository.UpdateAsync(userDto);
    }

    private static ValidationFailure[] GenerateValidationError(string message) 
    {
        return new[] 
        { 
            new ValidationFailure(nameof(User), message) 
        };
    }
}

