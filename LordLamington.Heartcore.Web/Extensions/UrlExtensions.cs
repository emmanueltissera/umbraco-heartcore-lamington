namespace LordLamington.Heartcore.Web.Extensions
{
    public static class UrlExtensions
    {
        public static string ToSafeUrl(this string url)
        {
            return url;
            //return url.Replace("/home/","/");
        }
    }
}
