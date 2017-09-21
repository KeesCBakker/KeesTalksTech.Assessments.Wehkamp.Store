using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using System;
using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping
{
    public interface IBasket
    {
        string Id { get; }

        DateTime? Created { get; set; }

        DateTime? Modified { get; set; }

        IReadOnlyList<IBasketItem> Contents { get; }

        IBasketItem Add(IProduct product, uint amount);

        void Remove(IBasketItem item);
    }
}