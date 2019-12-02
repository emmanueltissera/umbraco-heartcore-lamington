using System;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Models;
using LordLamington.Heartcore.Web.Mvc;
using Umbraco.Headless.Client.Net.Delivery;

namespace LordLamington.Heartcore.Web.Services
{
    public class SettingsService
    {
        private readonly ContentDeliveryService _contentDeliveryService;
        private readonly UmbracoCache _umbracoCache;

        public SettingsService(ContentDeliveryService contentDeliveryService, UmbracoCache umbracoCache)
        {
            _contentDeliveryService = contentDeliveryService ?? throw new ArgumentNullException(nameof(contentDeliveryService));
            _umbracoCache = umbracoCache ?? throw new ArgumentNullException(nameof(umbracoCache));
            RootNode = GetHomeNode().Result;
        }

        private async Task<Home> GetHomeNode()
        {
            var rootContent = await _umbracoCache.GetContentByUrl("/");
            return new Home(rootContent);
        }

        public Home RootNode { get; set; }
    }
}
