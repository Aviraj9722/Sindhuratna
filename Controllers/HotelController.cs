using Microsoft.AspNetCore.Mvc;
using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace DistrictInfoAppv2.Controllers
{
    public class HotelController : Controller
    {
        private readonly IUmbracoContextAccessor _contextAccessor;

        public HotelController(IUmbracoContextAccessor contextAccessor)
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

            IPublishedContent? hotel =
                umbracoContext.Content.GetById(id);

            if (hotel == null)
                return NotFound();
            var hotelPage = hotel as Hotel;
            ViewBag.HotelPage = hotelPage;
            return View();
        }
    }
}
