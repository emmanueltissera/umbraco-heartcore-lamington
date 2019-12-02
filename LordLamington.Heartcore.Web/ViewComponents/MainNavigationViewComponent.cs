using System;
using System.Linq;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Extensions;
using LordLamington.Heartcore.Web.Models;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery;

namespace LordLamington.Heartcore.Web.ViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly ContentDeliveryService _contentDeliveryService;
        private readonly UmbracoCache _umbracoCache;

        public MainNavigationViewComponent(ContentDeliveryService contentDeliveryService, UmbracoCache umbracoCache)
        {
            _contentDeliveryService = contentDeliveryService ?? throw new ArgumentNullException(nameof(contentDeliveryService));
            _umbracoCache = umbracoCache ?? throw new ArgumentNullException(nameof(umbracoCache));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rootContent = await _umbracoCache.GetContentByUrl("/");
            var homeNode = new Home(rootContent);
            var children = await _contentDeliveryService.Content.GetChildren(rootContent.Id);

            var navItems = children.Content.Items.Where(x => x.IsVisible())
                .Select(item => new NavigationItem
                {
                    Title = item.Name, Url = item.Url.Replace("/home/","/"), IsCurrent = item.Url == Request.Path.ToString()
                }).ToList();

            navItems.Insert(0, new NavigationItem { Title = rootContent.Name, Url = "/", IsCurrent = "/" == Request.Path.ToString() });

            var navViewModel = new NavigationViewModel()
            {
                IsHomePage = "/" == Request.Path.ToString(),
                NavigationItems = navItems,
                Root = homeNode
            };

            return View(navViewModel);
        }
    }
}
