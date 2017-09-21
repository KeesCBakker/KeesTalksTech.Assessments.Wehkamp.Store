using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models
{
    public class ProductModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public string PictureUrl { get; set; }

        public PriceModel Price { get;  set; }
        
        public List<string> Keywords { get; set; }
    }
}