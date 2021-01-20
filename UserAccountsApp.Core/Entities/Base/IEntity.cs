using System;

namespace UserAccountsApp.Core.Entities.Base
{
    public interface IEntity
    {
        long Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Modified { get; set; }
        bool IsDeleted { get; set; }
    }
}