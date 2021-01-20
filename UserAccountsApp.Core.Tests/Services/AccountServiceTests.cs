using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccountsApp.Core.Configuration;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Repositories.Users;
using UserAccountsApp.Core.Services;
using UserAccountsApp.Core.Tests.Mocks;
using UserAccountsApp.Core.Validation;

namespace UserAccountsApp.Core.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests : MvxTest
    {
        private User user;
        private IAccountService service;
        private IEntityValidator<User> validator;
        private IUserRepository repository;
        private IDataContext context;
        private IDbConfig config;

        [SetUp]
        public async Task SetUp()
        {
            ClearAll();

            config = new DbConfig(":memory:");
            context = new SqliteDataContext(config);
            repository = new UserRepository(context);
            validator = new UserValidator();
            service = new AccountService(repository, validator);
            user = EntityBuilder.CreateUser();

            await context.Initialization;
            await context.Db.ExecuteAsync("DELETE FROM User");
        }

        [Test]
        public async Task CreateUser_SavesValidUser()
        {
            var result = await service.CreateUser(user);
            var findCreatedUserResult = await service.GetUserById(user.Id);

            Assert.IsTrue(result.IsSuccess, $"Failed to create test user");
            Assert.IsTrue(findCreatedUserResult.IsSuccess, $"Reason: {findCreatedUserResult.FailureMessage} \r\r Value: {findCreatedUserResult.Value}");
            Assert.IsNotNull(findCreatedUserResult.Value, $"User was null: {findCreatedUserResult.FailureMessage}");
        }

        [Test]
        public async Task CreateUser_PreventsSavingInvalidUsers()
        {
            var invalidUserList = new List<User>
            {
                EntityBuilder.CreateInvalidFirstNameUserList(1)[0],
                EntityBuilder.CreateInvalidLastNameUserList(1)[0],
                EntityBuilder.CreateInvalidPhoneUserList(1)[0],
                EntityBuilder.CreateInvalidPasswordUserList(1)[0],
                EntityBuilder.CreateInvalidServiceStartDateUserList(1)[0]
            };

            invalidUserList.AsParallel().ForAll(async x =>
            {
                var result = await service.CreateUser(x);
                Assert.IsFalse(result.IsSuccess, $"Failed to prevent saving invalid user data: {JsonConvert.SerializeObject(x)}");
            });

            var dbUserList = await repository.GetAllAsync();
            Assert.IsTrue(!dbUserList.Any(), $"Failed to prevent saving invalid user data {dbUserList.Count} times \r\r Values: {JsonConvert.SerializeObject(dbUserList)}");
        }

        [Test]
        public async Task GetAll_ReturnsExpectedAmountOfResults()
        {
            var validUserList = new List<User>
            {
                EntityBuilder.CreateUser(username: "user1"),
                EntityBuilder.CreateUser(username: "user2"),
                EntityBuilder.CreateUser(username: "user3")
            };

            validUserList.AsParallel().ForAll(async x =>
            {
                var result = await service.CreateUser(x);
                Assert.IsTrue(result.IsSuccess, $"Failed to save valid user data: {x} \r\r Reason: {result.FailureMessage}");
            });

            var dbUserList = await repository.GetAllAsync();
            Assert.IsTrue(dbUserList.Count == validUserList.Count, $"Failed to save valid user data \r\r Found Values: {JsonConvert.SerializeObject(dbUserList)} " +
                $"\r\r Expected Values: {JsonConvert.SerializeObject(validUserList)}");
        }
    }
}