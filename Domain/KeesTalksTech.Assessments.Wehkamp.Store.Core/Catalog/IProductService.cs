using System;
using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog
{
    public interface IProductService
    {
        IProduct Get(string id);

        ///<remarks>Should be CQRS - but it's only an assessment</remarks>
        IList<IProduct> GetAll(uint page, uint pageSize);

        ///<remarks>Should be CQRS - but it's only an assessment</remarks>
        IList<IProduct> Search(string search, uint page, uint pageSize);

        IList<IProduct> Random(uint count);
    }
}
