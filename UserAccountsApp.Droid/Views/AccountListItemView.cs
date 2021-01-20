using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using UserAccountsApp.Core.ViewModels;

namespace UserAccountsApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Account List Item")]
    public class AccountListItemView : MvxActivity<AccountListItemViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.accountlistitem_view);
        }
    }
}