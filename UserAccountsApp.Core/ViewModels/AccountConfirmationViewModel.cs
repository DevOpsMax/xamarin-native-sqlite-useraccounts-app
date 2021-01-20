using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace UserAccountsApp.Core.ViewModels
{
    public class AccountConfirmationViewModel : BaseViewModel
    {
        private string _accountCreationSuccessMessage;

        public AccountConfirmationViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            AccountCreationSuccessMessage = "Account successfully created!";
            ContinueCommand = new MvxAsyncCommand(GoToSignInPage);
        }

        private async Task GoToSignInPage()
        {
            IsBusy = true;
            await NavigationService.Navigate<SignInViewModel>();
        }

        public IMvxAsyncCommand ContinueCommand { get; private set; }

        public string AccountCreationSuccessMessage
        {
            get => _accountCreationSuccessMessage;
            set => SetProperty(ref _accountCreationSuccessMessage, value);
        }
    }
}