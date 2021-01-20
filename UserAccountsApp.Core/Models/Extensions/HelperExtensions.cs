using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UserAccountsApp.Core.Models.Extensions
{
    public static class HelperExtensions
    {
        private static readonly List<string> _notAllowedList = new List<string> { "(", "!", "@", "#", "$", "%", "^", "&", ")" };
        private static readonly Regex _findRepetitiveSequence = new Regex(@"(.{3,})\1|(.)\2\2", RegexOptions.IgnoreCase);
        private static readonly Regex _validatePhoneNumber = new Regex(@"^\(\d{3}\)-\d{3}-\d{4}$");

        public static bool IsValueSanitized(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && _notAllowedList.TrueForAll(x => !value.Contains(x));
        }

        public static bool IsLengthValid(this string value, int min, int max)
        {
            return !string.IsNullOrWhiteSpace(value) && value.Length >= min && value.Length <= max;
        }

        public static bool ContainsUpperChar(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.Any(char.IsUpper);
        }

        public static bool ContainsLowerChar(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.Any(char.IsLower);
        }

        public static bool IsDistinctWords(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && !_findRepetitiveSequence.IsMatch(value);
        }

        public static bool IsPhoneValid(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && _validatePhoneNumber.IsMatch(value);
        }

        public static bool IsPasswordValid(this string value)
        {
            return value.IsLengthValid(8, 15)
                && value.ContainsLowerChar()
                && value.ContainsUpperChar()
                && value.IsDistinctWords();
        }

        public static bool IsServiceStartDateValid(this DateTimeOffset value)
        {
            var serviceUtcDate = value.UtcDateTime.Date;
            var yesterdayUtcDate = DateTimeOffset.UtcNow.AddDays(-1).Date;
            var cutoffUtcDate = DateTimeOffset.UtcNow.AddDays(30).Date;

            return serviceUtcDate > yesterdayUtcDate
                && serviceUtcDate <= cutoffUtcDate;
        }
    }
}