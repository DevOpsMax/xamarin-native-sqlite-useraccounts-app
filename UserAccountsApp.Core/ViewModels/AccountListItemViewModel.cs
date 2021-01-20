using MvvmCross.ViewModels;

namespace UserAccountsApp.Core.ViewModels
{
    public class AccountListItemViewModel : MvxViewModel
    {
        private string _username;
        private string _combinedName;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string CombinedName
        {
            get => _combinedName;
            set => SetProperty(ref _combinedName, value);
        }
    }
}