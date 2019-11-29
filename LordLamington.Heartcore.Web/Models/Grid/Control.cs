using Newtonsoft.Json.Linq;

namespace LordLamington.Heartcore.Web.Models.Grid
{
    public class Control
    {
        public bool Active { get; set; }

        public object Value { get; set; }

        public Editor Editor { get; set; }
    }
}
