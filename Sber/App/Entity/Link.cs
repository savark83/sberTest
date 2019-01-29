namespace Sber.App.Entity
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Bson;

    /// <summary>
    /// Ссылка
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Сокращенная ссылка
        /// </summary>
        public string ShortValue { get; set; }

        /// <summary>
        /// Полная ссылка
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Счетчик переходов
        /// </summary>
        public int HitCount { get; set; }

        /// <summary>
        /// Идентификаторы пользователей, которые сокращали ссылку 
        /// </summary>
        public List<Guid> UsersIds { get; set; }

    }
}
