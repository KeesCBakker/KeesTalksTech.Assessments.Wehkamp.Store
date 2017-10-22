using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using RestSharp;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Shopping
{
    internal class BasketsNode : IBasketsNode
    {
        private readonly IStoreSdkClientSettings _settings;

        public BasketsNode(IStoreSdkClientSettings settings)
        {
            _settings = settings;
        }

        public async Task<BasketModel> GetAsync(string id)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/baskets/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            var result =  await client.Execute<BasketModel>(request);

            return result.Data;
        }

        public async Task<BasketModel> CreateAsync()
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/baskets", Method.PUT);

            var result = await client.Execute<BasketModel>(request);

            return result.Data;
        }

        public async Task<BasketItemModel> AddToBasketAsync(string basktetId, string productId, uint amount)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/baskets/{basktetId}/items", Method.PUT);
            request.AddUrlSegment("basktetId", basktetId);
            request.AddObject(new
            {
                productId = productId,
                amount = amount
            });

            var result = await client.Execute<BasketItemModel>(request);

            return result.Data;
        }

        public async Task<bool> RemoveBasketItemAsync(string basktetId, string basketItemId)
        {
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("/api/baskets/{basktetId}/items/{basketItemId}", Method.DELETE);
            request.AddUrlSegment("basktetId", basktetId);
            request.AddUrlSegment("basketItemId", basketItemId);

            var result = await client.Execute(request);

            return true;
        }
    }
}
