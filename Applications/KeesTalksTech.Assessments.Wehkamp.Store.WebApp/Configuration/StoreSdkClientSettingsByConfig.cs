using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient;
using Microsoft.Extensions.Configuration;
using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Configuration
{
    public class StoreSdkClientSettingsByConfig : StoreSdkClientSettings, IStoreSdkClientSettings
    {
        const string BaseUrlAppSettingKey = "StoreSdkClient.BaseUrl";

        public StoreSdkClientSettingsByConfig(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            BaseUrl = config[BaseUrlAppSettingKey];

            if (String.IsNullOrEmpty(BaseUrl))
            {
                throw new Exception("Invalid configuration. AppSetting StoreSdkClient.BaseUrl is missing.");
            }
        }
    }
}