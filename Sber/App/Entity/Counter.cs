namespace Sber.App.Entity
{
    using MongoDB.Bson;

    /// <summary>
    /// Счетчик последовательностей
    /// </summary>
    public class Counter
    {
        public ObjectId Id { get; set; }

        public string Type { get; set; }

        public int SequenceValue { get; set; }
    }
}
