using Moq;
using MvvmCross.Navigation;
using NUnit.Framework;
using System.Threading.Tasks;
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
    public class CreateAccountViewModelTests : MvxTest
    {
        private CreateAccountViewModel viewModel;
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

            viewModel = new CreateAccountViewModel(navigation.Object, service, validator);

            // raise all property change events on current thread
            viewModel.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
        }

        [Test]
        public async Task CreateAccount_ValidatesAndCreatesNewUserWithoutErrors()
        {
            // stub out clean user object
            var validUser = EntityBuilder.CreateUser();

            // set vm props and confirm submit button is disabled
            viewModel.Username = validUser.Username;
            Assert.IsFalse(viewModel.CreateAccountCommand.CanExecute());

            viewModel.Password = validUser.Password;
            Assert.IsFalse(viewModel.CreateAccountCommand.CanExecute());

            viewModel.ServiceStartDate = validUser.ServiceStartDate;
            Assert.IsFalse(viewModel.CreateAccountCommand.CanExecute());

            viewModel.Phone = validUser.Phone;
            Assert.IsFalse(viewModel.CreateAccountCommand.CanExecute());

            viewModel.FirstName = validUser.FirstName;
            Assert.IsFalse(viewModel.CreateAccountCommand.CanExecute());

            viewModel.LastName = validUser.LastName;
            Assert.IsTrue(viewModel.CreateAccountCommand.CanExecute());

            // execute button command
            await viewModel.CreateAccountCommand.ExecuteAsync();

            // confirm error did not occur
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.ErrorMessage));

            // new user saved successfully
            var users = await repository.GetAllAsync();
            Assert.IsTrue(users.Count == 1);
        }
    }
}