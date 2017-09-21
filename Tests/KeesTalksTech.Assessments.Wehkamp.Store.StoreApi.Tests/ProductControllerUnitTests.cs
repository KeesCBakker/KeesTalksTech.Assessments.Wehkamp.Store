using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Controllers;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Tests
{
    [TestClass]
    public class ProductControllerUnitTests
    {
        [TestMethod]
        public void ProductController_Get()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Id = "1", Name = "Alpha" });
            repo.Add(new PhotoProduct { Id = "2", Name = "Beta" });
            repo.Add(new PhotoProduct { Id = "3", Name = "Gamma" });

            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get("2");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(ProductModel));

            var product = okResult.Value as ProductModel;

            Assert.IsNotNull(product, "Product 2 should be present.");
            Assert.AreEqual("Beta", product.Name, "Name of the second product should be Beta.");
        }

        [TestMethod]
        public void ProductController_Get_NotFound()
        {
            var repo = new PhotoProductMemoryRepository();
            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get("Hoi");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ProductController_GetAll()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Id = "1", Name = "Alpha" });
            repo.Add(new PhotoProduct { Id = "2", Name = "Beta" });
            repo.Add(new PhotoProduct { Id = "3", Name = "Gamma" });

            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get();

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<ProductModel>));

            var list = (okResult.Value as IEnumerable<ProductModel>).ToList();

            Assert.AreEqual(3, list.Count, "List count should be 3.");

            Assert.AreEqual("Alpha", list[0].Name, "First element should be Alpha.");
            Assert.AreEqual("Beta", list[1].Name, "Second element should be Beta.");
            Assert.AreEqual("Gamma", list[2].Name, "Second element should be Gamma.");
        }

        [TestMethod]
        public void ProductController_GetAll_Paging()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Id = "1", Name = "Alpha" });
            repo.Add(new PhotoProduct { Id = "2", Name = "Beta" });
            repo.Add(new PhotoProduct { Id = "3", Name = "Gamma" });

            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get(page: 2, pageSize: 1);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<ProductModel>));

            var list = (okResult.Value as IEnumerable<ProductModel>).ToList();

            Assert.AreEqual(1, list.Count, "List count should be 1.");
            Assert.AreEqual("Beta", list[0].Name, "First element should be Beta.");
        }

        [TestMethod]
        public void ProductController_GetAll_Search()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Name = "Alpha" });
            repo.Add(new PhotoProduct { Name = "Beta" });
            repo.Add(new PhotoProduct { Name = "Gamma" });
            repo.Add(new PhotoProduct { Name = "Delta" });

            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get(page: 1, pageSize:99, search: "ta");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<ProductModel>));

            var list = (okResult.Value as IEnumerable<ProductModel>).ToList();

            Assert.AreEqual(2, list.Count, "List count should be 2.");

            Assert.AreEqual("Beta", list[0].Name, "First element should be Beta.");
            Assert.AreEqual("Delta", list[1].Name, "Second element should be Delta.");
        }

        [TestMethod]
        public void ProductController_GetAll_SearchPaging()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Name = "Alpha" });
            repo.Add(new PhotoProduct { Name = "Beta" });
            repo.Add(new PhotoProduct { Name = "Gamma" });
            repo.Add(new PhotoProduct { Name = "Delta" });

            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get(page: 2, pageSize: 1, search: "ta");

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult.Value, "Value should not be null.");
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<ProductModel>));

            var list = (okResult.Value as IEnumerable<ProductModel>).ToList();

            Assert.AreEqual(1, list.Count, "List count should be 1.");

            Assert.AreEqual("Delta", list[0].Name, "First element should be Delta.");
        }

        [TestMethod]
        public void ProductController_GetAll_InvalidPage_BadRequest()
        {
            var repo = new PhotoProductMemoryRepository();
            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get(page: 0);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void ProductController_GetAll_InvalidPageSize_BadRequest()
        {
            var repo = new PhotoProductMemoryRepository();
            var service = new PhotoProductService(repo);
            var controller = new ProductsController(service);

            var result = controller.Get(pageSize: 0);

            Assert.IsNotNull(result, "There should be a result.");
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
