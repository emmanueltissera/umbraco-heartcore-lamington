using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Refit;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.Mvc
{
  public class UmbracoContext
    {
        public UmbracoContext(UmbracoCache cache)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public UmbracoCache Cache { get; }
        public IContent Content { get; private set; }

        public string Language { get; private set; }

        internal async Task<bool> RouteUmbracoContentAsync(HttpContext context)
        {
            var url = context?.Request?.Path.Value;
            if (url == null)
                throw new InvalidOperationException("Could not determine the current URL path in the request");

            try
            {
                Language = url.StartsWith("/sinhala/") ? "si-LK" : "en-AU";
                var content = await Cache.GetContentByUrl(url, Language);
                Content = content;
                return content != null;
            }
            catch(ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
        }
    }
}
