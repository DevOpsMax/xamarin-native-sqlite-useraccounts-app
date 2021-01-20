using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Tests;
using MvvmCross.Views;

namespace UserAccountsApp.Core.Tests.Mocks
{
    public class MvxTest : MvxIoCSupportingTest
    {
        private static IMvxIoCProvider IoCProvider => MvxSingleton<IMvxIoCProvider>.Instance;

        protected MockMvxViewDispatcher CreateMockNavigation()
        {
            var dispatcher = new MockMvxViewDispatcher();
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            return dispatcher;
        }
    }
}