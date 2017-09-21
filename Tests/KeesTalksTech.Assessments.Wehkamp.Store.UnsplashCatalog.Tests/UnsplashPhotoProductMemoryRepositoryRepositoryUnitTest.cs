using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Tests
{
    [TestClass]
    public class UnsplashPhotoProductMemoryRepositoryRepositoryUnitTest
    {
        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_Add()
        {
            var product = new PhotoProduct { Name = "Alpha" };

            var repo = new PhotoProductMemoryRepository();
            repo.Add(product);

            var repoProduct = repo.Get(product.Id);

            Assert.AreSame(product, repoProduct, "Objects should be the same.");
        }

        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_Add_Update()
        {
            var initialProduct = new PhotoProduct { Name = "Alpha" };

            var repo = new PhotoProductMemoryRepository();
            repo.Add(initialProduct);

            var updateProduct = new PhotoProduct { Id = initialProduct.Id, Name = "Beta" };

            repo.Add(updateProduct);

            var repoProduct = repo.Get(updateProduct.Id);

            Assert.IsNotNull(repoProduct, "Product should be present in the repository.");
            Assert.AreEqual("Beta", repoProduct.Name, "Product should now be called Beta.");

            Assert.AreSame(updateProduct, repoProduct, "Objects should be the same.");

            Assert.AreNotSame(initialProduct, updateProduct, "Objects should be the same.");
        }

        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_Get_NotExist()
        {
            var repo = new PhotoProductMemoryRepository();
            var product = repo.Get("Hoi");

            Assert.IsNull(product, "Repository should be empty.");
        }

        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_GetAll()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Name = "Alpha" });
            repo.Add(new PhotoProduct { Name = "Beta" });
            repo.Add(new PhotoProduct { Name = "Gamma" });

            var list = repo.GetAll(1, 9999);

            Assert.AreEqual(3, list.Count, "List count should be 3.");

            Assert.AreEqual("Alpha", list[0].Name, "First element should be Alpha.");
            Assert.AreEqual("Beta", list[1].Name, "Second element should be Beta.");
            Assert.AreEqual("Gamma", list[2].Name, "Second element should be Gamma.");
        }

        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_GetAll_Paging()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Name = "Alpha" });
            repo.Add(new PhotoProduct { Name = "Beta" });
            repo.Add(new PhotoProduct { Name = "Gamma" });

            var list = repo.GetAll(2, 1);

            Assert.AreEqual(1, list.Count, "List count should be 1.");

            Assert.AreEqual("Beta", list[0].Name, "First element should be Beta.");
        }

        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_Search()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Name = "Alpha" });
            repo.Add(new PhotoProduct { Name = "Beta" });
            repo.Add(new PhotoProduct { Name = "Gamma" });
            repo.Add(new PhotoProduct { Name = "Delta" });

            var list = repo.Search("ta", 1, 9999);

            Assert.AreEqual(2, list.Count, "List count should be 2.");

            Assert.AreEqual("Beta", list[0].Name, "First element should be Beta.");
            Assert.AreEqual("Delta", list[1].Name, "Second element should be Delta.");
        }

        [TestMethod]
        public void PhotoProductMemoryRepositoryRepository_Search_Paging()
        {
            var repo = new PhotoProductMemoryRepository();
            repo.Add(new PhotoProduct { Name = "Alpha" });
            repo.Add(new PhotoProduct { Name = "Beta" });
            repo.Add(new PhotoProduct { Name = "Gamma" });
            repo.Add(new PhotoProduct { Name = "Delta" });

            var list = repo.Search("ta", 2, 1);

            Assert.AreEqual(1, list.Count, "List count should be 1.");

            Assert.AreEqual("Delta", list[0].Name, "First element should be Delta.");
        }
    }
}
