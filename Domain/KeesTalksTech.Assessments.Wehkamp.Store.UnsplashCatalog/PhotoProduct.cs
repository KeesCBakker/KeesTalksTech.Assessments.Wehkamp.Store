using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog
{
    public class PhotoProduct : IProduct
    {
        public PhotoProduct()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public PhotoProductPrice Price { get; set; } = new PhotoProductPrice();

        public string PictureUrl { get; set; }

        public string[] Keywords { get; set; }

        public string Orientation { get; set; }

        IPrice IProduct.Price => Price;
    }
}
