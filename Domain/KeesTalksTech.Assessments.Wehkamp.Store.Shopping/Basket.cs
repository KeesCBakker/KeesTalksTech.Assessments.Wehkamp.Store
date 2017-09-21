using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;
using System;
using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping
{
    public class Basket : IBasket
    {
        private readonly List<BasketItem> _contents = new List<BasketItem>();

        public string Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public IReadOnlyList<BasketItem> Contents => _contents.AsReadOnly();

        public BasketItem Add(IProduct product, uint amount)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            //why add 0?
            if (amount == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), 0, "Minimal order amount is 1.");
            }

            var item = new BasketItem
            {
                Amount = amount,
                Content = product,
                Created = DateTime.Now
            };

            _contents.Add(item);

            return item;
        }

        public void Remove(BasketItem item)
        {
            if (!_contents.Contains(item))
            {
                throw new ArgumentOutOfRangeException(nameof(item));
            }

            _contents.Remove(item);
        }


        #region IBasket interface

        public void Remove(IBasketItem item)
        {
            var basketItem = item as BasketItem;

            if (basketItem == null)
            {
                throw new ArgumentException(nameof(item), "Type not supported.");
            }

            Remove(basketItem);
        }

        IBasketItem IBasket.Add(IProduct product, uint amount)
        {
            return Add(product, amount);
        }

        IReadOnlyList<IBasketItem> IBasket.Contents => Contents;

        #endregion
    }
}
