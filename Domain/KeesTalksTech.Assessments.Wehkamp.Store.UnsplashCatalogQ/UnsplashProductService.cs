using System;
using System.Linq;
using System.Collections.Generic;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Utilities.Settings;
using Unsplasharp;
using Unsplasharp.Models;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Sdk
{
    public class UnsplashProductService : IProductService
    {
        UnsplashApiSettings _settings;

        public UnsplashProductService()
        {
            _settings = GetSettingsFromConfiguration();
        }

        public UnsplashProductService(UnsplashApiSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public IProduct Get(string key)
        {
            var client = CreateClient();
            EnsureRateLimit(client);
            var task = client.GetPhoto(key);

            var result = task.Result;

            if(result == null)
            {
                return null;
            }

            var product = Map(result);
            return product;
        }

        public IList<UnsplashProduct> GetAll(uint page, uint pageSize)
        {
            return Search("", page, pageSize);
        }

        public IList<UnsplashProduct> Search(string search, uint page, uint pageSize)
        {
            var client = CreateClient();
            EnsureRateLimit(client);

            var task = client.SearchPhotos(search, (int)page, (int)pageSize);

            var query = task.Result.Select(p => Map(p));

            return query.ToList();
        }

        private static UnsplashProduct Map(Photo photo)
        {
            return new UnsplashProduct
            {
                Key = photo.Id,
                Description = photo.Description,
                Name = photo.Description.Substring(0, 20) + " by " + photo.User.Name,
                Thumbnail = photo.Urls.Thumbnail
            };
        }

        private void EnsureRateLimit(UnsplasharpClient client)
        {
            if (client.RateLimitRemaining == 0)
            {
                throw new Exception("Rate limit!");
            }
        }

        private UnsplasharpClient CreateClient()
        {
            return new UnsplasharpClient(_settings.ApplicationId, _settings.ApplicationSecret);
        }

        private static UnsplashApiSettings GetSettingsFromConfiguration()
        {
            return AppSettingsProvider.Create<UnsplashApiSettings>();
        }

        IList<IProduct> IProductService.GetAll(uint page, uint pageSize)
        {
            var list = GetAll(page, pageSize);
            return list.AsEnumerable<IProduct>().ToList();
        }

        IList<IProduct> IProductService.Search(string search, uint page, uint pageSize)
        {
            var list = Search(search, page, pageSize);
            return list.AsEnumerable<IProduct>().ToList();
        }
    }
}
