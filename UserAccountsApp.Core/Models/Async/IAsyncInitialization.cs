using System.Threading.Tasks;

namespace UserAccountsApp.Core.Models.Async
{
    public interface IAsyncInitialization
    {
        Task<bool> Initialization { get; }
    }
}