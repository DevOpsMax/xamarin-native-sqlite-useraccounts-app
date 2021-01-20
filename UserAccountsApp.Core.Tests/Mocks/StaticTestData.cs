using System;
using System.Collections.Generic;

namespace UserAccountsApp.Core.Tests.Mocks
{
    public class StaticTestData
    {
        public static List<string> InvalidFirstNameList = new List<string>
        {
            "Max(",
            "Max!",
            "Max@",
            "Max#",
            "Max$",
            "Max%",
            "Max^",
            "Max&",
            "Max)"
        };

        public static List<string> InvalidLastNameList = new List<string>
        {
            "Larson(",
            "Larson!",
            "Larson@",
            "Larson#",
            "Larson$",
            "Larson%",
            "Larson^",
            "Larson&",
            "Larson)"
        };

        public static List<string> InvalidPasswordList = new List<string>
        {
            "2short",
            "passwordToooLong",
            "Password111",
            "000Password",
            "TESTtest",
            "TeSttEsT",
            "testabcabc",
            "zzabcabczz",
            "12ab12abtest",
            string.Empty,
            " "
        };

        public static List<string> InvalidPhoneList = new List<string>
        {
            "(111) 111-1111",
            "111)-111-1111",
            "1112223333",
            "(111)-111-abCD",
            "(111-111-111",
            "(111)-1111111",
            "(111)-111-1111 ",
            " (111)-111-1111",
            string.Empty,
            " "
        };

        public static List<DateTimeOffset> InvalidServiceStartDateList = new List<DateTimeOffset>
        {
            DateTimeOffset.UtcNow.AddDays(31),
            DateTimeOffset.UtcNow.AddDays(-1),
            DateTimeOffset.MinValue,
            DateTimeOffset.MaxValue
        };
    }
}