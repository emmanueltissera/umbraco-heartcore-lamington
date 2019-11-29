using System.Collections.Generic;

namespace LordLamington.Heartcore.Web.Models.Grid
{
    public class Area
    {
        public List<Control> Controls { get; set; }
        public bool Active { get; set; }
        public bool HasConfig { get; set; }
        public int Grid { get; set; }
    }
}
