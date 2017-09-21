using RestSharp;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient.Rest
{
    public static class RestExtensions
    {
        /// <summary>
        /// Executes the given request the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(this IRestClient client, IRestRequest request) where T : new()
        {
            var tcs = new TaskCompletionSource<T>();
            client.ExecuteAsync<T>(request, response => {

                //todo: error handling

                tcs.SetResult(response.Data);
            });
            return await tcs.Task;
        }


        /// <summary>
        /// Executes the given request the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static async Task<bool> ExecuteAsync(this IRestClient client, IRestRequest request)
        {
            var tcs = new TaskCompletionSource<bool>();
            client.ExecuteAsync(request, response => {
                
                //todo: error handling

                tcs.SetResult(true);
            });
            return await tcs.Task;
        }
    }
}
