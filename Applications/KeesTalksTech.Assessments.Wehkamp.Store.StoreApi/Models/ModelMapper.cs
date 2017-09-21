using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models
{
    internal static class ModelMapper
    {
        public static ProductModel MapProduct(IProduct product)
        {
            if (product == null)
            {
                return null;
            }

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                ThumbnailUrl = product.ThumbnailUrl,
                Price = MapPrice(product.Price),
                Keywords = MapKeywords(product.Keywords)
            };
        }

        public static List<string> MapKeywords(IEnumerable<string> keywords)
        {
            if(keywords == null)
            {
                return new List<string>();
            }

            return keywords.ToList();
        }

        public static PriceModel MapPrice(IPrice price)
        {
            if(price == null)
            {
                return null;
            }

            return new PriceModel
            {
                Amount = price.Amount
            };
        }

        public static BasketItemModel MapBasketItem(IBasketItem basketItem)
        {
            if(basketItem == null)
            {
                return null;
            }

            return new BasketItemModel
            {
                Amount = basketItem.Amount,
                Content = MapProduct(basketItem.Content),
                Id = basketItem.Id,
                Created = basketItem.Created,
                Modified = basketItem.Modified
            };
        }

        public static List<BasketItemModel> MapBasketItems(IEnumerable<IBasketItem> basketItems)
        {
            if (basketItems == null)
            {
                return null;
            }

            return basketItems
                    .Select(bi => MapBasketItem(bi))
                    .ToList();
        }

        public static BasketModel MapBasket(IBasket basket)
        {
            if(basket == null)
            {
                return null;
            }

            return new BasketModel
            {
                Id = basket.Id,
                Created = basket.Created,
                Modified = basket.Modified,
                Contents = MapBasketItems(basket.Contents.AsEnumerable())
            };
        }

    }
}
