using System;
using System.Linq;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Models;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.ViewComponents
{
    public class ProductCollectionViewComponent : ViewComponent
    {
        private readonly UmbracoContext _umbracoContext;

        public ProductCollectionViewComponent(UmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public async Task<IViewComponentResult> InvokeAsync(Content productRoot = null)
        {
            var isCollectionPage = true;
            if (productRoot == null)
            {
                var productRootPaged = await _umbracoContext.Cache.GetByType(ProductCollectionViewModel.ContentTypeAlias, _umbracoContext.Language);
                productRoot = productRootPaged.Content.Items.FirstOrDefault();
                isCollectionPage = false;
            }

            if (productRoot == null)
            {
                throw new Exception("Product Root is null");
            }

            var children = await _umbracoContext.Cache.GetChildren(productRoot.Id, _umbracoContext.Language);
            var productRootNode = new ProductCollectionViewModel(productRoot, children, isCollectionPage);

            return View(productRootNode);
        }
    }
}
