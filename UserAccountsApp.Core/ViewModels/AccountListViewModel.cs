using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using UserAccountsApp.Core.Services;

namespace UserAccountsApp.Core.ViewModels
{
    public class AccountListViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;
        private MvxObservableCollection<AccountListItemViewModel> _listItems;        

        public AccountListViewModel(IMvxNavigationService navigationService, IAccountService accountService) : base(navigationService)
        {
            _accountService = accountService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            var result = await _accountService.GetUsers();

            if (result.IsSuccess)
            {
                var items = result.Value.Select(x => new AccountListItemViewModel
                {
                    CombinedName = $"{x.FirstName} {x.LastName}",
                    Username = x.Username
                })
                .OrderBy(x => x.CombinedName);

                ListItems = new MvxObservableCollection<AccountListItemViewModel>(items);
            }
            else
            {
                ErrorMessage = result.FailureMessage;
            }
        }

        public MvxObservableCollection<AccountListItemViewModel> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
    }
}