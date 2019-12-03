using System;
using System.Linq;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Extensions;
using LordLamington.Heartcore.Web.Models;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace LordLamington.Heartcore.Web.ViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly UmbracoContext _umbracoContext;

        public MainNavigationViewComponent(UmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rootContent = await _umbracoContext.Cache.GetContentByUrl("/", _umbracoContext.Language);
            var homeNode = new Home(rootContent);
            var children = await _umbracoContext.Cache.GetChildren(rootContent.Id, _umbracoContext.Language);

            var navItems = children.Content.Items.Where(x => x.IsVisible())
                .Select(item => new NavigationItem
                {
                    Title = item.Name, Url = item.Url.ToSafeUrl(), IsCurrent = item.Url == Request.Path.ToString()
                }).ToList();

            navItems.Insert(0, new NavigationItem { Title = homeNode.Title, Url = rootContent.Url, IsCurrent = rootContent.Url == Request.Path.ToString() });

            var navViewModel = new NavigationViewModel()
            {
                IsHomePage = rootContent.Url == Request.Path.ToString(),
                NavigationItems = navItems,
                Root = homeNode
            };

            return View(navViewModel);
        }
    }
}
