using System;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace LordLamington.Heartcore.Web.Controllers
{
    public sealed class DefaultUmbracoController : UmbracoController
    {
        private readonly IViewEngine _viewEngine;

        public DefaultUmbracoController(UmbracoContext umbracoContext, ICompositeViewEngine viewEngine) : base(umbracoContext)
        {
            _viewEngine = viewEngine ?? throw new ArgumentNullException(nameof(viewEngine));
        }

        public IActionResult Index()
        {
            var viewName = UmbracoContext.Content.ContentTypeAlias;
            var result = _viewEngine.GetView("", viewName, true);
            if(!result.Success)
                result = _viewEngine.FindView(ControllerContext, viewName, true);
            if (!result.Success)
                viewName = "Index";

            return View(viewName, UmbracoContext.Content);
        }
    }
}
