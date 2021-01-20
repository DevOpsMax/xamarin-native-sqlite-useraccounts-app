using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace UserAccountsApp.Core.Tests.Mocks
{
	public class MockMvxViewDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
	{
		public List<IMvxViewModel> CloseRequests = new List<IMvxViewModel>();
		public List<MvxViewModelRequest> NavigateRequests = new List<MvxViewModelRequest>();

		private Func<bool, Task<bool>> ReturnMockedBoolTask = async (bool value) => await Task.Run(() => value);

		public override bool IsOnMainThread => throw new NotImplementedException();

		public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
		{
			action();
			return true;
		}

		Task<bool> IMvxViewDispatcher.ShowViewModel(MvxViewModelRequest request)
		{
			NavigateRequests.Add(request);
			return ReturnMockedBoolTask(true);
		}

		public virtual bool ChangePresentation(MvxPresentationHint hint)
		{
			throw new NotImplementedException();
		}

		Task<bool> IMvxViewDispatcher.ChangePresentation(MvxPresentationHint hint)
		{
			throw new NotImplementedException();
		}

		public Task ExecuteOnMainThreadAsync(Action action, bool maskExceptions = true)
		{
			throw new NotImplementedException();
		}

		public Task ExecuteOnMainThreadAsync(Func<Task> action, bool maskExceptions = true)
		{
			throw new NotImplementedException();
		}
	}
}