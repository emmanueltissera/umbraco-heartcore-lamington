using System;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace LordLamington.Heartcore.Web.Controllers
{
    public abstract class UmbracoController : Controller, IUmbracoController
    {
        public UmbracoController(UmbracoContext umbracoContext)
        {
            UmbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        protected UmbracoContext UmbracoContext { get; }
    }
}
