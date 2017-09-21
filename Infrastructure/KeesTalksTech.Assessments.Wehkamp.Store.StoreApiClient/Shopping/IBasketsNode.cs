using System.Threading.Tasks;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Shopping
{
    public interface IBasketsNode
    {
        Task<BasketItemModel> AddToBasketAsync(string basktetId, string productId, uint amount);

        Task<BasketModel> CreateAsync();

        Task<BasketModel> GetAsync(string id);

        Task<bool> RemoveBasketItemAsync(string basktetId, string basketItemId);
    }
}