using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Utilities.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog
{
    public class PhotoProductService : IProductService
    {
        private readonly IPhotoProductRepository _repository;

        public PhotoProductService(IPhotoProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PhotoProduct Get(string identifier)
        {
            return _repository.Get(identifier);
        }

        public IList<PhotoProduct> GetAll(uint page, uint pageSize)
        {
            //doesn't make sense - why request nothing?
            if (pageSize == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Minimal page size is 1.");
            }

            return _repository.GetAll(page, pageSize);
        }

        public IList<PhotoProduct> Random(uint count)
        {
            if(count > _repository.Count)
            {
                count = Convert.ToUInt32(_repository.Count);
            }

            var indexes = new List<int>();

            //generate - never do this for realz!
            while (true)
            {
                int nr = 0;

                //make sure the 1st photo is landscpae
                if(indexes.Count == 0)
                {
                    var landscapes = _repository.Query(pr => pr.Orientation == "l").ToList();
                    nr = Randomizer.GenerateRandomNumber(0, landscapes.Count - 1);
                    nr = landscapes[nr].Item1;
                }
                //make sure the 2nd and the 6th photo is portrait
                else if (indexes.Count == 1 || indexes.Count == 5)
                {
                    var portraits = _repository.Query(pr => pr.Orientation == "p").ToList();
                    nr = Randomizer.GenerateRandomNumber(0, portraits.Count - 1);
                    nr = portraits[nr].Item1;
                }
                else
                {
                    nr = Randomizer.GenerateRandomNumber(0, _repository.Count - 1);
                }

                if (!indexes.Contains(nr))
                {
                    indexes.Add(nr);

                    if (indexes.Count == count)
                    {
                        break;
                    }
                }
            }

            return
                   indexes
                    .Select(i => _repository.GetByIndex(i))
                    .ToList();
            
        }

        public IList<PhotoProduct> Search(string search, uint page, uint pageSize)
        {
            //doesn't make sense - why request nothing?
            if (pageSize == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Minimal page size is 1.");
            }

            return _repository.Search(search, page, pageSize);
        }

        IProduct IProductService.Get(string identifier)
        {
            return Get(identifier);
        }

        IList<IProduct> IProductService.GetAll(uint page, uint pageSize)
        {
            var list = GetAll(page, pageSize);
            return list.AsEnumerable<IProduct>().ToList();
        }

        IList<IProduct> IProductService.Random(uint count)
        {
            var list = Random(count);
            return list.AsEnumerable<IProduct>().ToList();
        }

        IList<IProduct> IProductService.Search(string search, uint page, uint pageSize)
        {
            var list = Search(search, page, pageSize);
            return list.AsEnumerable<IProduct>().ToList();
        }
    }
}
