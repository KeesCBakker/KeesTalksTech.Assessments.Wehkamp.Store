using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Shopping;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient
{
    public interface IStoreSdkClient
    {
        IProductsNode Products { get; }

        IBasketsNode Baskets { get; }
    }
}