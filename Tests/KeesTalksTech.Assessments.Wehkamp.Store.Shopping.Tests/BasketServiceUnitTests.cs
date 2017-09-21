using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping.Tests
{
    [TestClass]
    public class BasketServiceUnitTests
    {
        [TestMethod]
        public void BasketService_Get()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
                repo.Add(new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now });
                repo.Add(new Basket { Id = "Belgium", Created = DateTime.Now, Modified = DateTime.Now });
            var service = new BasketService(repo, productService);

            var basket = service.Get("Belgium");

            Assert.IsNotNull(basket, "Basket should not be null.");
            Assert.AreEqual("Belgium", basket.Id, "Basked should have ID Belgium");
        }

        [TestMethod]
        public void BasketService_Get_NotFound()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            repo.Add(new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now });
            repo.Add(new Basket { Id = "Belgium", Created = DateTime.Now, Modified = DateTime.Now });
            var service = new BasketService(repo, productService);

            var basket = service.Get("Congo");

            Assert.IsNull(basket, "Basket Congo should be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(BasketServiceException))]
        public void BasketService_Store_InvalidBasked()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            var service = new BasketService(repo, productService);

            //no identifier - this basket is not valid
            var basket = new Basket { Id = null };

            //this should fail
            service.Store(basket);
        }

        [TestMethod]
        public void BasketService_Validate_InvalidId()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            var service = new BasketService(repo, productService);

            var basket = new Basket { Id = "" };
            var validationResult = service.Validate(basket);

            Assert.IsFalse(validationResult, "Id is not set, so the basket should be invalid.");
        }

        [TestMethod]
        public void BasketService_Validate_InvalidProduct()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            var service = new BasketService(repo, productService);

            //1. create basket
            var basket = service.Create();

            //2. add invalid product
            var product = new PhotoProduct { Name = "I do not exist.", Id = "1337" };
            basket.Add(product, 1);
            
            //3. Validate it against the service
            var validationResult = service.Validate(basket);

            Assert.IsFalse(validationResult, "Id is not set, so the basket should be invalid.");
        }

        [TestMethod]
        public void BasketService_Validate_InvalidAmount()
        {
            var productRepo = new PhotoProductMemoryRepository();
                productRepo.Add(new PhotoProduct { Id = "42", Name = "Alpha" }); ;
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            var service = new BasketService(repo, productService);

            //1. create basket
            var basket = service.Create();

            //2. add product from product service
            var product = productService.Get("42");
            basket.Add(product, 1);

            //3. Change amount
            basket.Contents[0].Amount = 0;

            //3. Validate it against the service
            var validationResult = service.Validate(basket);

            Assert.IsFalse(validationResult, "Amount is 0, so the basket should be unvalid.");
        }
    }
}
