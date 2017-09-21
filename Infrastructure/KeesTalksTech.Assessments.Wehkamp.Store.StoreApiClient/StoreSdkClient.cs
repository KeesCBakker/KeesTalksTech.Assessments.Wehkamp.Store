using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Shopping;
using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient
{
    public class StoreSdkClient : IStoreSdkClient
    {
        private readonly IStoreSdkClientSettings _settings;
        private readonly ProductsNode _products;
        private readonly BasketsNode _baskets;

        public StoreSdkClient(IStoreSdkClientSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _products = new ProductsNode(settings);
            _baskets = new BasketsNode(settings);
        }

        public IProductsNode Products => _products;

        public IBasketsNode Baskets => _baskets;

    }
}
