using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;
using System;
using System.Linq;
using System.Collections.Generic;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping
{
    public class BasketService : IBasketService
    {
        public readonly IProductService _productService;
        public readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository, IProductService productService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public Basket Create()
        {
            var basket = new Basket
            {
                Id = Guid.NewGuid().ToString()
            };

            Store(basket);

            return basket;
        }

        public Basket Get(string identifier)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            return _repository.Get(identifier);
        }

        public IList<Basket> GetAll(uint page, uint pageSize)
        {
            return _repository.GetAll(page, pageSize);
        }

        public void Store(Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            if (!Validate(basket))
            {
                throw new BasketServiceException("Basket not valid.");
            }

            //should be rolled back when storing goes wrong
            basket.Created = basket.Created ?? DateTime.Now;
            basket.Modified = DateTime.Now;

            //update items as well
            basket.Contents.ToList().ForEach(item =>
            {
                item.Id = item.Id ?? Guid.NewGuid().ToString();
                item.Created = item.Created ?? DateTime.Now;
                item.Modified = DateTime.Now;
            });

            //store it
            _repository.Add(basket);
        }

        public void Store(IBasket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var b = basket as Basket;

            if (b == null)
            {
                throw new ArgumentException("Invalid type.", nameof(basket));
            }

            Store(b);
        }

        public bool Validate(Basket basket)
        {
            //validate business rules
            //should return like a validation result with the problems
            //but this is only a simple excersize, just return true/false 

            if (basket == null)
            {
                return false;
            }

            //validate identifier
            if (String.IsNullOrEmpty(basket.Id))
            {
                return false;
            }

            //validate if products are present
            if(basket.Contents.FirstOrDefault(c => c.Content == null) != null)
            {
                return false;
            } 

            //validate if products are excisting, query product service
            if(basket.Contents.FirstOrDefault(c => _productService.Get(c.Content.Id) == null) != null)
            {
                return false;
            }

            //validate amount
            if (basket.Contents.FirstOrDefault(c => c.Amount == 0) != null)
            {
                return false;
            }

            return true;
        }

        public bool Validate(IBasket basket)
        {
            return Validate(basket);
        }

        IBasket IBasketService.Create()
        {
            return Create();
        }

        IBasket IBasketService.Get(string identifier)
        {
            return Get(identifier);
        }

        IList<IBasket> IBasketService.GetAll(uint page, uint pageSize)
        {
            throw new NotImplementedException();
        }
    }
}