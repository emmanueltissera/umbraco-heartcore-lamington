using System.Collections.Generic;

namespace LordLamington.Heartcore.Web.Models
{
    public class NavigationViewModel
    {
        public IEnumerable<NavigationItem> NavigationItems { get; set; }

        public Home Root { get; set; }

        public bool IsHomePage { get; set; }
    }
}
