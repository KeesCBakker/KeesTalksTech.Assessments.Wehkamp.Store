using System;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping
{
    public interface IBasketItem
    {
        string Id { get; }

        IProduct Content { get; set; }

        uint Amount { get; set; }

        DateTime? Created { get; set; }

        DateTime? Modified { get; set; }
    }
}
