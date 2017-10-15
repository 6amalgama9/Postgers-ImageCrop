using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MainController: Controller
    {
        public ActionResult Index()
        {
            var form = new FormModel();
            return View(form);
        }

        [HttpPost]
        public JsonResult PostData()
        {
            var inValue = Request.Form["value"];
            var inId = int.Parse(Request.Form["id"]);
            using (var db = new DataContext())
            {
                var field = db.Field.FirstOrDefault(p => p.Id == inId);
                if (field == null)
                    return Json("Field was not found.");
                field.Data = inValue;
                return Json(db.SaveChanges());
            }
        }
    }
}