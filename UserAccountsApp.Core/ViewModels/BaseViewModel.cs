using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace UserAccountsApp.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService NavigationService;

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            IsBusy = false;
            ErrorMessage = string.Empty;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
    }
}