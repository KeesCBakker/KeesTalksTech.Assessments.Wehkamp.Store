using System.Collections.Generic;
using System.Threading.Tasks;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Catalog
{
    public interface IProductsNode
    {
        Task<IEnumerable<ProductModel>> SearchAsync(string search, uint page, uint pageSize);

        Task<IEnumerable<ProductModel>> GetAllAsync(uint page, uint pageSize);

        Task<ProductModel> GetAsync(string id);

        Task<IEnumerable<ProductModel>> RandomAsync(uint count);
    }
}