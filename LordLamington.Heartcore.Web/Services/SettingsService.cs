using System;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Models;
using LordLamington.Heartcore.Web.Mvc;

namespace LordLamington.Heartcore.Web.Services
{
    public class SettingsService
    {
        private readonly UmbracoContext _umbracoContext;

        public SettingsService(UmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
            RootNode = GetHomeNode().Result;
        }

        private async Task<Home> GetHomeNode()
        {
            var rootContent = await _umbracoContext.Cache.GetContentByUrl("/", _umbracoContext.Language);
            return new Home(rootContent);
        }

        public Home RootNode { get; set; }
    }
}
