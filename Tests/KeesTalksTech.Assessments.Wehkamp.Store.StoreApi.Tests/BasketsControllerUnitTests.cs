using KeesTalksTech.Assessments.Wehkamp.Store.Shopping;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Controllers;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Tests
{
    [TestClass]
    public class BasketsControllerUnitTests
    {
        [TestMethod]
        public void BasketsController_GetBasket()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);
            var repo = new BasketMemoryRepository();
            repo.Add(new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now });
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.Get("Aruba");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(BasketModel));

            var basket = okResult.Value as BasketModel;

            Assert.IsNotNull(basket, "The retrieved basket should not be null.");
            Assert.AreEqual("Aruba", basket.Id);
        }


        [TestMethod]
        public void BasketsController_GetBasket_NotFound()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);
            var repo = new BasketMemoryRepository();
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.Get("Congo");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BasketsController_AddBasketItem()
        {
            var productRepo = new PhotoProductMemoryRepository();
            productRepo.Add(new PhotoProduct { Name = "Alpha", Id = "42" });
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            repo.Add(new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now });
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.AddBasketItem("Aruba", "42", 1337);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(BasketItemModel));

            var item = okResult.Value as BasketItemModel;

            Assert.IsNotNull(item, "New basket item should be present");
            Assert.IsNotNull(item.Content, "New basket item content should be present.");
            Assert.AreEqual("42", item.Content.Id, "Content identifier should be 42.");
            Assert.AreEqual(1337u, item.Amount, "The amount should be 1337.");
        }

        [TestMethod]
        public void BasketsController_AddBasketItem_InvalidProduct_BadRequest()
        {
            var productRepo = new PhotoProductMemoryRepository();
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            repo.Add(new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now });
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.AddBasketItem("Aruba", "XXX", 1);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void BasketsController_AddBasketItem_InvalidAmount_BadRequest()
        {
            var productRepo = new PhotoProductMemoryRepository();
            productRepo.Add(new PhotoProduct { Name = "Alpha", Id = "42" });
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
            repo.Add(new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now });
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.AddBasketItem("Aruba", "42", 0);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void BasketsController_RemoveBasketItem()
        {
            //prepare dataset: a product, a basket and a basket item
            var product = new PhotoProduct { Name = "Alpha", Id = "42" };
            var basket = new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now };
            var basketItem = basket.Add(product, 1337);

            //prepare the ceremonies: the repo's, services and the controller
            var productRepo = new PhotoProductMemoryRepository();
                productRepo.Add(product);
            var productService = new PhotoProductService(productRepo);

            var repo = new BasketMemoryRepository();
                repo.Add(basket);

            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            //actual test
            var result = controller.RemoveBasketItem("Aruba", basket.Contents[0].Id);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkResult));

            //check result in original basket
            Assert.AreEqual(0, basket.Contents.Count, "There should be no basket items anymore.");
        }

        [TestMethod]
        public void BasketsController_RemoveBasketItem_InvalidBasket_BadRequest()
        {
            var productService = new PhotoProductService(new PhotoProductMemoryRepository());
            var repo = new BasketMemoryRepository();
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.RemoveBasketItem("Aruba", "XX");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void BasketsController_RemoveBasketItem_InvalidBasketItemId_NotFound()
        {
            var productService = new PhotoProductService(new PhotoProductMemoryRepository());
            var basket = new Basket { Id = "Aruba", Created = DateTime.Now, Modified = DateTime.Now };
            var repo = new BasketMemoryRepository();
                repo.Add(basket);
            var service = new BasketService(repo, productService);

            var controller = new BasketsController(service, productService);

            var result = controller.RemoveBasketItem("Aruba", "XX");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}