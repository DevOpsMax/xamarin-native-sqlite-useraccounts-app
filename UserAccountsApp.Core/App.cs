using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using UserAccountsApp.Core.Configuration;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Repositories.Users;
using UserAccountsApp.Core.Validation;
using UserAccountsApp.Core.Services;
using UserAccountsApp.Core.ViewModels;

namespace UserAccountsApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            // database
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDbConfig, DbConfig>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDataContext, SqliteDataContext>();

            // data validators
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IEntityValidator<User>, UserValidator>();

            // repositories
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IUserRepository, UserRepository>();

            // services
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAccountService, AccountService>();

            RegisterCustomAppStart<AppStart>();
        }
    }

    /// <summary>
    /// Custom app start object for performing auth/startup logic and decides the starting view model
    /// </summary>
    public class AppStart : MvxAppStart
    {
        private readonly IDataContext _dataContext;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IDataContext dataContext) : base(application, navigationService)
        {
            _dataContext = dataContext;
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            try
            {
                // ensure sqlite db initialization
                // todo: consider refactoring to async factory
                var tcs = new TaskCompletionSource<bool>();
                Task.Run(async () => tcs.SetResult(await _dataContext.Initialization));

                // todo: add authentication logic

                // navigate to first view
                return NavigationService.Navigate<SignInViewModel>();
            }
            catch (System.Exception exception)
            {
                throw exception.MvxWrap("Problem navigating to ViewModel {0}", typeof(SignInViewModel).Name);
            }
        }
    }
}