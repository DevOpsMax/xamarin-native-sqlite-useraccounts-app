using System;
using UserAccountsApp.Core.Models.Extensions;
using UserAccountsApp.Core.Models.Results;
using UserAccountsApp.Core.Entities;

namespace UserAccountsApp.Core.Validation
{
    public class UserValidator : IEntityValidator<User>
    {
        public IResult IsValid(User entity)
        {
            if (!entity.FirstName.IsValueSanitized())
                return Result.Failure("First Name must not have these special characters (!@#$%^&)");

            if (!entity.LastName.IsValueSanitized())
                return Result.Failure("Last Name must not have these special characters (!@#$%^&)");

            if (!entity.Password.IsPasswordValid())
                return Result.Failure("Password must have from 8 to 15 characters, at least one lowercase letter, at least one uppercase letter, and must not have repetitive any sequence of characters(i.e. 'abcabc', '111', '12ab12ab' are not allowed)");

            if (!entity.Phone.IsPhoneValid())
                return Result.Failure("Phone must use the following format: (XXX)-XXX-XXXX");

            if (!entity.ServiceStartDate.IsServiceStartDateValid())
                return Result.Failure($"Service Start Date must be after {DateTimeOffset.UtcNow.AddDays(-1).Date} and before {DateTimeOffset.UtcNow.AddDays(31).Date}");

            return Result.Success();
        }
    }
}