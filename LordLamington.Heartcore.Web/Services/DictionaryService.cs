using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LordLamington.Heartcore.Web.Config;
using LordLamington.Heartcore.Web.Extensions;
using LordLamington.Heartcore.Web.Mvc;
using Microsoft.Extensions.Options;

namespace LordLamington.Heartcore.Web.Services
{
    public class DictionaryService
    {
        private readonly UmbracoContext _umbracoContext;
        private readonly ProjectOptions _projectOptions;

        public DictionaryService(UmbracoContext umbracoContext, IOptions<ProjectOptions> projectOptions)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
            _projectOptions = projectOptions.Value ?? throw new ArgumentNullException(nameof(ProjectOptions));
            Items = GetDictionary().Result;
        }

        private async Task<Dictionary<string,string>> GetDictionary()
        {
            var children = await _umbracoContext.Cache.GetChildren(_projectOptions.NodeSettings.DictionaryNodeGuid, _umbracoContext.Language, pageSize: 100);

            return children.Content.Items.ToDictionary(t => t.Value<string>("key"), t=> t.Value<string>("value"));
        }

        public Dictionary<string, string> Items { get; set; }

        public Dictionary<string, string> Languages => GetLanguages();

        private Dictionary<string, string> GetLanguages()
        {
            return new Dictionary<string, string>()
            {
                { Items["Cultures.en-au"], "/english/" },
                { Items["Cultures.si-lk"], "/sinhala/" }
            };
        }
    }
}
