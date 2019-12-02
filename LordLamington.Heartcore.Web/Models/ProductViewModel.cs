using System;
using LordLamington.Heartcore.Web.Extensions;
using Microsoft.AspNetCore.Html;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.Models
{
    public class ProductViewModel
    {
        private const string ContentTypeAlias = "product";
        private readonly Content _content;

        public ProductViewModel(IContent content)
        {
            if (content.ContentTypeAlias != ContentTypeAlias)
            {
                throw new TypeLoadException($"Expected {ContentTypeAlias}. Got {content.ContentTypeAlias}");
            }

            _content = content as Content;
        }

        public decimal Price => decimal.Parse(_content.Value<string>("price"));

        public string Title => _content.Value<string>("title");

        public IHtmlContent Description => _content.Value<IHtmlContent>("description");

        public Image Image => _content.Value<Image>("image");
    }
}
