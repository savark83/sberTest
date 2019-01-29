namespace Sber.App.Services
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;
    using Sber.App.Entity;
    using Sber.App.Helpers;

    /// <inheritdoc />
    /// <summary>
    /// Сервис ссылок
    /// </summary>
    public class LinksService : ILinksService
    {
        private readonly IMongoCollection<Link> _links;
        private readonly IMongoCollection<Counter> _counters;

        public LinksService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("sber");
            this._links = database.GetCollection<Link>("links");
            this._counters = database.GetCollection<Counter>("counters");
        }

        /// <summary>
        /// Получить список ссылок сокращенных пользователем
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Link> GetByUser(Guid userId)
        {
            return this._links.Find(Builders<Link>.Filter.AnyEq(l => l.UsersIds, userId)).ToList();
        }

        /// <summary>
        /// Получить ссылку
        /// </summary>
        /// <param name="shortValue"></param>
        /// <returns></returns>
        public Link Get(string shortValue)
        {
            return this._links.FindOneAndUpdate(
                l => l.ShortValue == shortValue,
                Builders<Link>.Update.Inc(l => l.HitCount, 1));
        }

        /// <summary>
        /// Создать ссылку
        /// </summary>
        /// <param name="linkValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Link Create(string linkValue, Guid userId)
        {
            //ищем уже существующую
            var link = this._links.FindOneAndUpdate(
                l => l.Value == linkValue,
                Builders<Link>.Update.AddToSet(l => l.UsersIds, userId));

            if (link != null)
                return link;

            //если не нашли, то создаем новую ссылку
            var counter = this._counters.FindOneAndUpdate(
                c => c.Type == "links",
                Builders<Counter>.Update.Inc(c => c.SequenceValue, 1));

            var addedLink = new Link
            {
                Value = linkValue,
                HitCount = 0,
                ShortValue = LinksHelper.GetShortValue(counter.SequenceValue),
                UsersIds = new List<Guid> { userId }
            };

            this._links.InsertOne(addedLink);

            return addedLink;
        }
    }
}