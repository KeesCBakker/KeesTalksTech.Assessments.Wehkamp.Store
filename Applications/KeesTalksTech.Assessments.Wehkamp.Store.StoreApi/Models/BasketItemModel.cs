using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models
{
    public class BasketItemModel
    {
        public string Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public uint Amount { get; set; }

        public ProductModel Content { get; set; }

    }
}
