using UserAccountsApp.Core.Models.Results;

namespace UserAccountsApp.Core.Validation
{
    public interface IEntityValidator<TEntity>
    {
        IResult IsValid(TEntity entity);
    }
}