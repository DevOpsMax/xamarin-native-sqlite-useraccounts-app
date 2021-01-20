using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Tests.Mocks;
using UserAccountsApp.Core.Validation;

namespace UserAccountsApp.Core.Tests.Validation
{
    [TestFixture]
    public class UserValidatorTests : MvxTest
    {
        private User user;
        private IEntityValidator<User> validator;

        [SetUp]
        public void SetUp()
        {
            ClearAll();
            user = EntityBuilder.CreateUser();
            validator = new UserValidator();
        }

        [Test]
        public void UserAccountValidator_ValidateCleanTestUserObject()
        {
            ExecuteUserValidationTest();
        }

        [Test]
        public void UserAccountValidator_VerifyUserPhoneValidation()
        {
            ExecuteUserValidationTest(EntityBuilder.CreateInvalidPhoneUserList());
        }

        [Test]
        public void UserAccountValidator_VerifyUserFirstNameValidation()
        {
            ExecuteUserValidationTest(EntityBuilder.CreateInvalidFirstNameUserList());
        }

        [Test]
        public void UserAccountValidator_VerifyUserLastNameValidation()
        {
            ExecuteUserValidationTest(EntityBuilder.CreateInvalidLastNameUserList());
        }

        [Test]
        public void UserAccountValidator_VerifyUserPasswordValidation()
        {
            ExecuteUserValidationTest(EntityBuilder.CreateInvalidPasswordUserList());
        }

        [Test]
        public void UserAccountValidator_VerifyUserServiceStartDateValidation()
        {
            ExecuteUserValidationTest(EntityBuilder.CreateInvalidServiceStartDateUserList());
        }

        private void ExecuteUserValidationTest(List<User> userList = null)
        {
            // test user should always pass validation
            var validResult = validator.IsValid(user);
            Assert.IsTrue(validResult.IsSuccess, $"Failed to validate test user \r\r Reason: {validResult.FailureMessage}");

            if (userList != null)
            {
                userList.AsParallel().ForAll(x =>
                {
                    // test validation against invalid user objects
                    var invalidResult = validator.IsValid(x);
                    Assert.IsFalse(invalidResult.IsSuccess, $"User validation failure: {JsonConvert.SerializeObject(x)} \r\r Reason: {invalidResult.FailureMessage}");
                });
            }
        }
    }
}