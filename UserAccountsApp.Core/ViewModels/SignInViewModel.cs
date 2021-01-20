using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using UserAccountsApp.Core.Services;

namespace UserAccountsApp.Core.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;

        private string _username;
        private string _password;

        public SignInViewModel(IMvxNavigationService navigationService, IAccountService accountService) : base(navigationService)
        {
            _accountService = accountService;

            SignInCommand = new MvxAsyncCommand(TrySignIn, IsSignInEnabled());
            CreateAccountCommand = new MvxAsyncCommand(GoToCreateAccountPage, () => !IsBusy);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            // reset form
            Username = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;

            // reset button status
            SignInCommand.RaiseCanExecuteChanged();
            CreateAccountCommand.RaiseCanExecuteChanged();
        }

        private async Task TrySignIn()
        {
            var result = await _accountService.AttemptUserSignIn(_username, _password);

            if (result.IsSuccess)
                await NavigationService.Navigate<AccountListViewModel>();
            else
                ErrorMessage = result.FailureMessage;
        }

        private Func<bool> IsSignInEnabled()
        {
            // todo: add email address regex for validation
            return () => !IsBusy && !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
        }

        private async Task GoToCreateAccountPage()
        {
            IsBusy = true;
            await NavigationService.Navigate<CreateAccountViewModel>();
        }

        public IMvxAsyncCommand SignInCommand { get; private set; }
        public IMvxAsyncCommand CreateAccountCommand { get; private set; }

        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }
    }
}