namespace Sber.App.Services
{
    using System;
    using System.Collections.Generic;
    using Sber.App.Entity;

    /// <summary>
    /// Интерфейс сервиса ссылок
    /// </summary>
    public interface ILinksService
    {
        IEnumerable<Link> GetByUser(Guid userId);

        Link Get(string shortValue);

        Link Create(string linkValue, Guid userId);
    }
}
