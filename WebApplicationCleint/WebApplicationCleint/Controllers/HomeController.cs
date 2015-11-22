using System.Threading.Tasks;
using System.Web.Mvc;
using Gateway.Http;
using WebApplicationCleint.Models;

namespace WebApplicationCleint.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Denied(Roles="Administrator,User,Member")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Denied(Roles = "Member")]
        public ActionResult Member()
        {
            ViewBag.Message = "Member Only";

            return View();
        }

        // [Authorize(Roles = "members, admin")]
        [Denied(Roles = "Administrator")]
        public async Task<ActionResult> Contact()
        {
            var values = await WebApiService.Instance.GetAsync<string[]>("/api/Values");
           return View(values);
        }
    }
}