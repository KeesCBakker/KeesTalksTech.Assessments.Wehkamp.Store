using System;
using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models
{
    public class BasketModel
    {
        public string Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public List<BasketItemModel> Contents { get; set; }
    }
}
