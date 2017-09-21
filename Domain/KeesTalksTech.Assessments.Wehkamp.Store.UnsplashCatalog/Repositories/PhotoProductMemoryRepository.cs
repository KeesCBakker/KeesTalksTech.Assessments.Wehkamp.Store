using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories
{
    public class PhotoProductMemoryRepository : IPhotoProductRepository
    {
        private readonly List<string> _index = new List<string>();
        private readonly ConcurrentDictionary<string, PhotoProduct> _storage = new ConcurrentDictionary<string, PhotoProduct>();

        public PhotoProductMemoryRepository()
        {
        }

        public int Count
        {
            get { return _storage.Count; }
        }

        ///<remarks>Validation should have been done by the service. There's no validation here.</remarks>
        public void Add(PhotoProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            //ensure identifier
            if (String.IsNullOrEmpty(product.Id))
            {
                product.Id = Guid.NewGuid().ToString();
            }

            //make sure it is in the index
            if (!_index.Contains(product.Id))
            {
                _index.Add(product.Id);
            }

            //just add it
            _storage[product.Id] = product;
        }

        public PhotoProduct Get(string key)
        {
            return _storage.Values.FirstOrDefault(sp => sp.Id == key);
        }

        public PhotoProduct GetByIndex(int index)
        {
            var key = _index[index];
            return _storage[key];
        }

        public IList<PhotoProduct> GetAll(uint page, uint pageSize)
        {
            return Search(null, page, pageSize);
        }

        public IList<PhotoProduct> Search(string search, uint page, uint pageSize)
        {
            var query = _storage.Values.AsQueryable();

            //order by name - should be handled by CQRS or some pattern
            query = query.OrderBy(p => p.Name);

            //implement rudimentary full text search
            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(sp =>
                    (sp.Keywords != null && sp.Keywords.Contains(search, StringComparer.OrdinalIgnoreCase)) ||
                    (sp.Name != null && sp.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1) ||
                    (sp.Description != null && sp.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1)
                );
            }

            if (page > 1)
            {
                var skip = (page - 1) * pageSize;
                query = query.Skip((int)skip);
            }

            query = query.Take((int)pageSize);

            return query.ToList();
        }

        public IEnumerable<Tuple<int, PhotoProduct>> Query(Func<PhotoProduct, bool> predicate)
        {
            return _storage
                .Values
                .Where(predicate)
                .Select(value => new { Index = _index.IndexOf(value.Id), Product = value })
                .Select(value => new Tuple<int, PhotoProduct>(value.Index, value.Product));
        }
    }
}