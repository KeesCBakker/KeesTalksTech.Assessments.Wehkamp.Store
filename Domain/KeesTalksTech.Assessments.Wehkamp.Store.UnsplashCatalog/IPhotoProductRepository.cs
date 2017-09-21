using System;
using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog
{
    public interface IPhotoProductRepository
    {
        int Count { get; }

        PhotoProduct Get(string identifier);

        PhotoProduct GetByIndex(int i);

        IList<PhotoProduct> Search(string search, uint page, uint pageSize);

        IList<PhotoProduct> GetAll(uint page, uint pageSize);

        IEnumerable<Tuple<int, PhotoProduct>> Query(Func<PhotoProduct, bool> predicate);
    }
}
