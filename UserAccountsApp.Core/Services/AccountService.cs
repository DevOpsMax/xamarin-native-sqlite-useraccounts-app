using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAccountsApp.Core.Models.Results;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Repositories.Users;
using UserAccountsApp.Core.Validation;

namespace UserAccountsApp.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEntityValidator<User> _userAccountValidator;

        public AccountService(IUserRepository userRepository, IEntityValidator<User> userAccountValidator)
        {
            _userRepository = userRepository;
            _userAccountValidator = userAccountValidator;
        }

        public async Task<IResult> AttemptUserSignIn(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return Result.Failure("Username and password cannot be empty");

                var userExists = await _userRepository.TryFindUserByUsername(username);

                if (!userExists)
                    return Result.Failure("User not found");

                // todo: refactor to use salt/hash instead of plain text passwords
                var user = await _userRepository.TryFindUserByCredentials(username, password);

                if (user == null)
                    return Result.Failure("Password incorrect");

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<IValueResult<List<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                return ValueResult<List<User>>.Success(users);
            }
            catch (Exception ex)
            {
                return ValueResult<List<User>>.Failure($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<IValueResult<User>> GetUserById(long id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                return ValueResult<User>.Success(user);
            }
            catch (Exception ex)
            {
                return ValueResult<User>.Failure($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<IResult> CreateUser(User user)
        {
            try
            {
                var userValidationResult = _userAccountValidator.IsValid(user);

                if (!userValidationResult.IsSuccess)
                    return Result.Failure(userValidationResult.FailureMessage);

                await _userRepository.CreateAsync(user);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Something went wrong: {ex.Message}");
            }
        }
    }
}