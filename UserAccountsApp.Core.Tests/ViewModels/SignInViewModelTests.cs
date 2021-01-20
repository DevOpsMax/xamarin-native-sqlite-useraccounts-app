using System.Threading.Tasks;
using Moq;
using MvvmCross.Navigation;
using NUnit.Framework;
using UserAccountsApp.Core.Configuration;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Repositories.Users;
using UserAccountsApp.Core.Services;
using UserAccountsApp.Core.Tests.Mocks;
using UserAccountsApp.Core.Validation;
using UserAccountsApp.Core.ViewModels;

namespace UserAccountsApp.Core.Tests.ViewModels
{
    [TestFixture]
    public class SignInViewModelTests : MvxTest
    {
        private SignInViewModel viewModel;
        private IAccountService service;
        private IEntityValidator<User> validator;
        private IUserRepository repository;
        private IDataContext context;
        private IDbConfig config;
        private Mock<IMvxNavigationService> navigation;

        [SetUp]
        public async Task SetUp()
        {
            ClearAll();

            navigation = new Mock<IMvxNavigationService>();
            config = new DbConfig(":memory:");
            context = new SqliteDataContext(config);
            repository = new UserRepository(context);
            validator = new UserValidator();
            service = new AccountService(repository, validator);

            await context.Initialization;
            await context.Db.ExecuteAsync("DELETE FROM User");

            viewModel = new SignInViewModel(navigation.Object, service);

            // raise all property change events on current thread
            viewModel.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
        }

        [Test]
        public async Task SignIn_NonExistingUserCannotSignIn()
        {
            viewModel.Username = "TestUser123";
            viewModel.Password = "Password123*";

            // confirm button enabled
            Assert.IsTrue(viewModel.SignInCommand.CanExecute());

            // execute button command
            await viewModel.SignInCommand.ExecuteAsync();

            // check for error message
            Assert.IsTrue(!string.IsNullOrEmpty(viewModel.ErrorMessage));
        }

        [Test]
        public async Task SignIn_ExistingUserCanSignIn()
        {
            // setup vm
            viewModel.Username = "TestUser123";
            viewModel.Password = "Password123*";

            // add new user to db
            var user = EntityBuilder.CreateUser(username: viewModel.Username, password: viewModel.Password);
            await service.CreateUser(user);

            Assert.IsTrue(viewModel.SignInCommand.CanExecute());
            await viewModel.SignInCommand.ExecuteAsync();
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.ErrorMessage), viewModel.ErrorMessage);
        }

        [Test]
        public async Task SignIn_ExistingUserWithInvalidPasswordCannotSignIn()
        {
            viewModel.Username = "TestUser123";
            viewModel.Password = "Password123*";

            // add new user to db
            var user = EntityBuilder.CreateUser(username: viewModel.Username, password: viewModel.Password);
            await service.CreateUser(user);

            // set invalid password for this user
            viewModel.Password = "Pass";

            await viewModel.SignInCommand.ExecuteAsync();

            Assert.IsTrue(!string.IsNullOrEmpty(viewModel.ErrorMessage));
        }

        [Test]
        public void SignIn_CommandDisabledWhenUsernameNotSet()
        {
            viewModel.Username = string.Empty;
            viewModel.Password = "Password123*";

            // confirm button disabled
            Assert.IsFalse(viewModel.SignInCommand.CanExecute());
        }

        [Test]
        public void SignIn_CommandDisabledWhenPasswordNotSet()
        {
            viewModel.Username = "TestUser123";
            viewModel.Password = string.Empty;

            // confirm button disabled
            Assert.IsFalse(viewModel.SignInCommand.CanExecute());
        }
    }
}