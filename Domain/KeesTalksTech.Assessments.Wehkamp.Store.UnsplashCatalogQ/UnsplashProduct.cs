using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Sdk
{
    public class UnsplashProduct : IProduct
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public IPrice Price => new UnsplashPrice();
    }
}
