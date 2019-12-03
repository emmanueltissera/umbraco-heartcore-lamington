using System;
using LordLamington.Heartcore.Web.Extensions;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.Models
{
    public class Home
    {
        private const string ContentTypeAlias = "home";
        private readonly Content _content;

        public Home(IContent content)
        {
            if (content.ContentTypeAlias != ContentTypeAlias)
            {
                throw new TypeLoadException($"Expected {ContentTypeAlias}. Got {content.ContentTypeAlias}");
            }
            _content = content as Content;
        }

        public Image BannerImage => _content.Value<Image>("bannerImage");

        public string SubHeading => _content.Value<string>("subHeading");

        public string Headline => _content.Value<string>("headline");

        public string LocaleEmailAddress => _content.Value<string>("localeEmailAddress");

        public Image LocaleLogo => _content.Value<Image>("localeLogo");

        public string LocalePhoneNumber => _content.Value<string>("localePhoneNumber");

        public string Title => _content.Value<string>("title");

        public string Url => _content.Url;

    }
}
