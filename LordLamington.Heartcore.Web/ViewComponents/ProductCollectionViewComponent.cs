using System;
using System.Linq;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Models;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.ViewComponents
{
    public class ProductCollectionViewComponent : ViewComponent
    {
        private readonly ContentDeliveryService _contentDeliveryService;
        private readonly UmbracoCache _umbracoCache;

        public ProductCollectionViewComponent(ContentDeliveryService contentDeliveryService, UmbracoCache umbracoCache)
        {
            _contentDeliveryService = contentDeliveryService ?? throw new ArgumentNullException(nameof(contentDeliveryService));
            _umbracoCache = umbracoCache ?? throw new ArgumentNullException(nameof(umbracoCache));
        }

        public async Task<IViewComponentResult> InvokeAsync(Content productRoot = null)
        {
            var isCollectionPage = true;
            if (productRoot == null)
            {
                var productRootPaged = await _contentDeliveryService.Content.GetByType("productCollection");
                productRoot = productRootPaged.Content.Items.FirstOrDefault();
                isCollectionPage = false;
            }

            if (productRoot == null)
            {
                throw new Exception("Product Root is null");
            }

            var children = await _contentDeliveryService.Content.GetChildren(productRoot.Id);
            var productRootNode = new ProductCollectionViewModel(productRoot, children, isCollectionPage);

            return View(productRootNode);
        }
    }
}
