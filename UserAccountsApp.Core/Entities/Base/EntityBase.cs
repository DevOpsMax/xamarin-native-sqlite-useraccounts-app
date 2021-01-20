using SQLite;
using System;

namespace UserAccountsApp.Core.Entities.Base
{
    public class EntityBase : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Modified { get; set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; set; }
    }
}