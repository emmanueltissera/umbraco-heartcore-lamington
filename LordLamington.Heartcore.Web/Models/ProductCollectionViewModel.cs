using System;
using System.Collections.Generic;
using System.Linq;
using LordLamington.Heartcore.Web.Extensions;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.Models
{
    public class ProductCollectionViewModel
    {
        public const string ContentTypeAlias = "productCollection";
        private readonly Content _content;

        public ProductCollectionViewModel(IContent content, PagedContent pagedContent, bool isCollectionPage = false)
        {
            if (content.ContentTypeAlias != ContentTypeAlias)
            {
                throw new TypeLoadException($"Expected {ContentTypeAlias}. Got {content.ContentTypeAlias}");
            }

            _content = content as Content;
            IsCollectionPage = isCollectionPage;
            Products = pagedContent.Content.Items.Select(p => new ProductViewModel(p));
        }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public string Title => _content.Value<string>("title");

        public string SubTitle => _content.Value<string>("subTitle");

        public bool IsCollectionPage { get; set; }

        public string Url => _content.Url;
    }
}
