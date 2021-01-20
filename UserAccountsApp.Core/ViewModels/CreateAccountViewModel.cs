using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Validation;
using UserAccountsApp.Core.Services;

namespace UserAccountsApp.Core.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IEntityValidator<User> _userEntityValidator;
        private User _newUser;

        public CreateAccountViewModel(IMvxNavigationService navigationService, IAccountService accountService,
            IEntityValidator<User> userEntityValidator) : base(navigationService)
        {
            _accountService = accountService;
            _userEntityValidator = userEntityValidator;

            _newUser = new User { ServiceStartDate = DateTimeOffset.Now };
            CreateAccountCommand = new MvxAsyncCommand(TryCreateUser, IsCreateUserEnabled());
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            
            CreateAccountCommand.RaiseCanExecuteChanged();
        }

        private async Task TryCreateUser()
        {
            var result = await _accountService.CreateUser(_newUser);

            if (result.IsSuccess)
                await NavigationService.Navigate<AccountConfirmationViewModel>();
            else
                ErrorMessage = result.FailureMessage;
        }

        private Func<bool> IsCreateUserEnabled()
        {
            return () => !IsBusy && _userEntityValidator.IsValid(_newUser).IsSuccess;
        }

        public IMvxAsyncCommand CreateAccountCommand { get; private set; }

        public string Username
        {
            get => _newUser.Username;
            set
            {
                _newUser.Username = value;
                FormFieldChanged("Username");
            }
        }

        public string Password
        {
            get => _newUser.Password;
            set
            {
                _newUser.Password = value;
                FormFieldChanged("Password");
            }
        }

        public string FirstName
        {
            get => _newUser.FirstName;
            set
            {
                _newUser.FirstName = value;
                FormFieldChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _newUser.LastName;
            set
            {
                _newUser.LastName = value;
                FormFieldChanged("LastName");
            }
        }

        public string Phone
        {
            get => _newUser.Phone;
            set
            {
                _newUser.Phone = value;
                FormFieldChanged("Phone");
            }
        }

        public DateTimeOffset ServiceStartDate
        {
            get => _newUser.ServiceStartDate;
            set
            {
                _newUser.ServiceStartDate = value;
                FormFieldChanged("ServiceStartDate");
            }
        }

        private void FormFieldChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
            CreateAccountCommand.RaiseCanExecuteChanged();
        }
    }
}