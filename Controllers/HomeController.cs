using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace DistrictInfoAppv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUmbracoContextAccessor _contextAccessor;

        public HomeController(IUmbracoContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            if (!_contextAccessor.TryGetUmbracoContext(out var umbCtx))
                return NotFound();

            var root = umbCtx.Content.GetAtRoot().FirstOrDefault();

            var districts = root?
                .DescendantsOfType(District.ModelTypeAlias)
                .OfType<District>()
                .ToList() ?? new List<District>();

            return View(districts);

        }

    }
}
