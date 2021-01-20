using System;
using System.Collections.Generic;
using System.Linq;
using UserAccountsApp.Core.Entities;

namespace UserAccountsApp.Core.Tests.Mocks
{
    public class EntityBuilder
    {
        public static User CreateUser(long id = 0, string username = "testUserName", string firstName = "Max", string lastName = "Larson", string password = "Password123*",
            string phone = "(111)-111-1111", DateTimeOffset? serviceStartDate = null, DateTimeOffset? created = null, DateTimeOffset? modified = null, bool isDeleted = false)
        {
            var now = DateTimeOffset.Now;

            return new User
            {
                Id = id,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Phone = phone,
                ServiceStartDate = serviceStartDate ?? now,
                Created = created ?? now,
                Modified = modified ?? now,
                IsDeleted = isDeleted
            };
        }

        public static List<User> CreateInvalidFirstNameUserList(int? takeAmount = null)
        {
            var amount = takeAmount ?? StaticTestData.InvalidFirstNameList.Count;

            return StaticTestData
                .InvalidFirstNameList
                .Take(amount)
                .Select(x => CreateUser(firstName: x))
                .ToList();
        }

        public static List<User> CreateInvalidLastNameUserList(int? takeAmount = null)
        {
            var amount = takeAmount ?? StaticTestData.InvalidLastNameList.Count;

            return StaticTestData
                .InvalidLastNameList
                .Select(x => CreateUser(lastName: x))
                .ToList();
        }

        public static List<User> CreateInvalidPasswordUserList(int? takeAmount = null)
        {
            var amount = takeAmount ?? StaticTestData.InvalidPasswordList.Count;

            return StaticTestData
                .InvalidPasswordList
                .Take(amount)
                .Select(x => CreateUser(password: x))
                .ToList();
        }

        public static List<User> CreateInvalidPhoneUserList(int? takeAmount = null)
        {
            var amount = takeAmount ?? StaticTestData.InvalidPhoneList.Count;

            return StaticTestData
                .InvalidPhoneList
                .Take(amount)
                .Select(x => CreateUser(phone: x))
                .ToList();
        }

        public static List<User> CreateInvalidServiceStartDateUserList(int? takeAmount = null)
        {
            var amount = takeAmount ?? StaticTestData.InvalidServiceStartDateList.Count;

            return StaticTestData
                .InvalidServiceStartDateList
                .Take(amount)
                .Select(x => CreateUser(serviceStartDate: x))
                .ToList();
        }
    }
}