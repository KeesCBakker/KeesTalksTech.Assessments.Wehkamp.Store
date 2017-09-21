using System;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping
{
    public class BasketItem : IBasketItem
    {
        public string Id { get; set; }

        public IProduct Content { get; set; }

        public uint Amount { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
