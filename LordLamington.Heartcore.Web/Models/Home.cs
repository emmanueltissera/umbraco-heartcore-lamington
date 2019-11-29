using LordLamington.Heartcore.Web.Extensions;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.Models
{
    public class Home
    {
        private readonly Content _content;

        public Home(IContent content)
        {
            _content = content as Content;
        }

        public Image BannerImage => _content.Value<Image>("bannerImage");

        public string SubHeading => _content.Value<string>("subHeading");

        public string Headline => _content.Value<string>("headline");

        public string LocaleEmailAddress => _content.Value<string>("localeEmailAddress");

        public Image LocaleLogo => _content.Value<Image>("localeLogo");

        public string LocalePhoneNumber => _content.Value<string>("localePhoneNumber");
    }
}
