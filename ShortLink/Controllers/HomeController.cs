using System.Threading.Tasks;
using System.Web.Mvc;
using ShortLink.Application.Services;

namespace ShortLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILinkService _linkService;

        public HomeController(ILinkService linkService)
        {
            _linkService = linkService;
        }
        // GET: Home
        public async Task<ActionResult> Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var link = await _linkService.GetLink(id);
                if (link != null)
                {
                    return Redirect(link);
                }
            }
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}