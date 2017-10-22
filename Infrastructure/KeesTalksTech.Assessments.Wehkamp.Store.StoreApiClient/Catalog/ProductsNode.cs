using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Catalog
{
    internal class ProductsNode : IProductsNode
    {
        private readonly IStoreSdkClientSettings _settings;

        public ProductsNode(IStoreSdkClientSettings settings)
        {
            _settings = settings;
        }

        public async Task<ProductModel> GetAsync(string id)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/products/{id}", Method.GET);
                request.AddUrlSegment("id", id);

            var result = await client.Execute<ProductModel>(request);

            return result.Data;
        }

        public async Task<IEnumerable< ProductModel>> GetAllAsync(uint page, uint pageSize)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/products/?page={page}&pageSize={pageSize}", Method.GET);
                request.AddUrlSegment("page", page);
                request.AddUrlSegment("pageSize", pageSize);

            var result = await client.Execute<List<ProductModel>>(request);

            return result.Data; 
        }

        public async Task<IEnumerable<ProductModel>> SearchAsync(string search, uint page, uint pageSize)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/products/?search={search}&page={page}&pageSize={pageSize}", Method.GET);
                request.AddUrlSegment("search", search);
                request.AddUrlSegment("page", page);
                request.AddUrlSegment("pageSize", pageSize);

            var result =  await client.Execute<List<ProductModel>>(request);

            return result.Data;
        }

        public async Task<IEnumerable<ProductModel>> RandomAsync(uint count)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/products/random?count={count}", Method.GET);
            request.AddUrlSegment("count", count);

            var result = await client.Execute<List<ProductModel>>(request);

            return result.Data;
        }
    }
}
