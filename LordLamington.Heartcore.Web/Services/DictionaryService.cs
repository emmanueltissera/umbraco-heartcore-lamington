using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Extensions;
using LordLamington.Heartcore.Web.Mvc;
using Umbraco.Headless.Client.Net.Delivery;

namespace LordLamington.Heartcore.Web.Services
{
    public class DictionaryService
    {
        private readonly ContentDeliveryService _contentDeliveryService;
        private readonly UmbracoCache _umbracoCache;

        public DictionaryService(ContentDeliveryService contentDeliveryService, UmbracoCache umbracoCache)
        {
            _contentDeliveryService = contentDeliveryService ?? throw new ArgumentNullException(nameof(contentDeliveryService));
            _umbracoCache = umbracoCache ?? throw new ArgumentNullException(nameof(umbracoCache));
            Items = GetDictionary().Result;
        }

        private async Task<Dictionary<string,string>> GetDictionary()
        {
            var dictionaryRootPaged = await _contentDeliveryService.Content.GetByType("dictionary");
            var dictionaryRoot = dictionaryRootPaged.Content.Items.FirstOrDefault();
            var children = await _contentDeliveryService.Content.GetChildren(dictionaryRoot.Id);

            return children.Content.Items.ToDictionary(t => t.Value<string>("key"), t=> t.Value<string>("value"));
        }

        public Dictionary<string, string> Items { get; set; }
    }
}
