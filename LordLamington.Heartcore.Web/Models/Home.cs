using Umbraco.Headless.Client.Net.Delivery.Models;

namespace LordLamington.Heartcore.Web.Models
{
    public class Home : ContentBase
    {
        public Image BannerImage { get; set; }

        public string SubHeading { get; set; }

        public string Headline { get; set; }
    }
}
