using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using System;
using UserAccountsApp.Core.ViewModels;

namespace UserAccountsApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Create Account", NoHistory = true)]
    public class CreateAccountView : MvxActivity<CreateAccountViewModel>
    {
        private static Button btnDatePicker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.createaccount_view);

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            btnDatePicker = FindViewById<Button>(Resource.Id.btnShowDatePicker);
            btnDatePicker.Click += BtnDatePicker_Click;
        }

        private void BtnDatePicker_Click(object sender, EventArgs e)
        {
            // todo: refactor to rely on accurate dateTime from server, it is possbile that the local device dateTime is invalid
            DateTimeOffset today = DateTimeOffset.Now;
            DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();
        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.ServiceStartDate = e.Date;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            btnDatePicker.Click -= BtnDatePicker_Click;
        }
    }
}