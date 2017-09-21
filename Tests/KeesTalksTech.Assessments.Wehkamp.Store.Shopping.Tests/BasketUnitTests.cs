using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping.Tests
{
    [TestClass]
    public class BasketUnitTests
    {
        [TestMethod]
        public void Basket_Content_Add()
        {
            var basket = new Basket { Id = "Aruba" };
            var product = new PhotoProduct { Name = "Alpha", Id = "42" };

            basket.Add(product, 1337);

            Assert.AreEqual(1, basket.Contents.Count, "There should be 1 item.");
            Assert.IsNotNull(basket.Contents[0], "There should be a basket item.");
            Assert.AreEqual(product, basket.Contents[0].Content, "The product should be in the first basket item.");
        }

        [TestMethod]
        public void Basket_Content_Remove()
        {
            var basket = new Basket { Id = "Aruba" };
            basket.Add(new PhotoProduct { Name = "Alpha", Id = "42" }, 1);
            basket.Add(new PhotoProduct { Name = "Beta", Id = "1337" }, 1);

            Assert.AreEqual(2, basket.Contents.Count, "There should be 2 items.");

            var betaItem = basket.Contents.FirstOrDefault(i => i.Content.Name == "Beta");

            Assert.IsNotNull(betaItem, "Item beta should be present.");

            basket.Remove(betaItem);

            Assert.AreEqual(1, basket.Contents.Count, "There should be 1 item.");
            Assert.AreEqual("Alpha", basket.Contents[0].Content.Name, "The basket item should be Alpha.");
        }

        [TestMethod]
        public void Basket_Content_RemoveInterfaceObject()
        {
            var basket = new Basket { Id = "Aruba" };
            basket.Add(new PhotoProduct { Name = "Alpha", Id = "42" }, 1);
            basket.Add(new PhotoProduct { Name = "Beta", Id = "1337" }, 1);

            Assert.AreEqual(2, basket.Contents.Count, "There should be 2 items.");

            IBasketItem betaItem = basket.Contents.FirstOrDefault(i => i.Content.Name == "Beta");

            Assert.IsNotNull(betaItem, "Item beta should be present.");

            basket.Remove(betaItem);

            Assert.AreEqual(1, basket.Contents.Count, "There should be 1 item.");
            Assert.AreEqual("Alpha", basket.Contents[0].Content.Name, "The basket item should be Alpha.");
        }
    }
}