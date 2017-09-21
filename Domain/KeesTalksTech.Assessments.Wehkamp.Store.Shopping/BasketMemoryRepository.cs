using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping
{
    public class BasketMemoryRepository : IBasketRepository
    {
        private readonly ConcurrentDictionary<string, Basket> _storage = new ConcurrentDictionary<string, Basket>();

        ///<remarks>Validation should have been done by the service. There's no validation here.</remarks>
        public void Add(Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            //just add it
            _storage[basket.Id] = basket;
        }

        public Basket Get(string key)
        {
            return _storage.Values.FirstOrDefault(sp => sp.Id == key);
        }

        public IList<Basket> GetAll(uint page, uint pageSize)
        {
            var query = _storage.Values.AsQueryable();

            //order by creation date - should be handled by CQRS or some pattern
            query = query.OrderBy(p => p.Created);

            if (page > 0)
            {
                var skip = (page - 1) * pageSize;
                query = query.Skip((int)skip);
            }

            query = query.Take((int)pageSize);

            return query.ToList();
        }
    }
}
