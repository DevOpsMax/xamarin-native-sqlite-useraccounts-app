using SQLite;
using System;
using UserAccountsApp.Core.Entities.Base;

namespace UserAccountsApp.Core.Entities
{
    public class User : EntityBase, IEntity
    {
        /// <summary>
        /// - Username must be unique
        /// </summary>
        [Unique]
        public string Username { get; set; }

        /// <summary>
        /// - First Name must not have these special characters (!@#$%^&)
        /// </summary> 
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// - Last Name must not have these special characters (!@#$%^&)
        /// </summary>
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// - Password must have from 8 to 15 characters
        /// - Password must have at least one lowercase letter
        /// - Password must have at least one uppercase letter
        /// - Password must not have repetitive any sequence of characters(i.e. 'abcabc', '111', '12ab12ab' are not allowed)
        /// </summary>
        [MaxLength(15)]
        public string Password { get; set; }

        /// <summary>
        /// - Phone field must use the following format => (XXX)-XXX-XXXX
        /// </summary>
        [MaxLength(14)]
        public string Phone { get; set; }

        /// <summary>
        /// - For Service Start Date, a past date is not allowed.
        /// - For Service Start Date, a value more than 30 days into the future should throw an error stating that is too early 
        ///   to create an account(i.e.Today - 10/03/2020 - Selected Date 11/04/2020 This scenario should throw an error).
        /// </summary>
        public DateTimeOffset ServiceStartDate { get; set; }
    }
}