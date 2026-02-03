using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace DistrictInfoAppv2.Controllers
{
    public class SchemeController : Controller
    {
        private readonly IUmbracoContextAccessor _contextAccessor;

        public SchemeController(IUmbracoContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {

            if (!_contextAccessor.TryGetUmbracoContext(out var umbracoContext))
                return StatusCode(500);

            IPublishedContent? Scheme =
                umbracoContext.Content.GetById(id);

            if (Scheme == null)
                return NotFound();
            var GovernmentSchemePage = Scheme as GovernmentScheme;
            ViewBag.GovernmentSchemePage = GovernmentSchemePage;
            return View();
        }
    }
}
