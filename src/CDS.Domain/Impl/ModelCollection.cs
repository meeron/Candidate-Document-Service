using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.Domain.Impl
{
    internal class ModelCollection<T> : ICollection<T>
        where T : ModelBase
    {
        private readonly IMongoCollection<T> _collection;

        internal ModelCollection(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public int Count
        {
            get
            {
                return (int)_collection.Count(new BsonDocument());
            }
        }

        public bool IsReadOnly { get { return false; } }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            item.ThrowIfInvalid();

            _collection.InsertOne(item);
        }

        public void Clear()
        {
            _collection.DeleteMany(new BsonDocument());
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var all = _collection.AsQueryable<T>().ToArray();
            for (int i = arrayIndex; i < all.Length; i++)
            {
                array[i] = all[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.AsQueryable<T>().GetEnumerator();
        }

        public bool Remove(T item)
        {
            return _collection.DeleteOne(Builders<T>.Filter.Eq(x => x.Id, item.Id)).DeletedCount > 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}