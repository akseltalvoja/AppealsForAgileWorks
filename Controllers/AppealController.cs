using AppealsProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppealsProject.Controllers
{
    public class AppealController : Controller
    {
        public static List<Appeal> _appeals = new List<Appeal>
        {
            new Appeal { AppealId = 1, AppealName = "Näidis pöördumine 2", AppealDescription = "Ei ole punane, kuna aega on rohkem kui üks tund", DueDateTime = DateTime.Now.AddDays(7) },
            new Appeal { AppealId = 2, AppealName = "Näidis Pöördumine 1", AppealDescription = "On punane, kuna aega on vähem kui üks tund", DueDateTime = DateTime.Now.AddDays(-1) }
        };



        public IActionResult Index()
        {
            var sortedAppeals = _appeals.OrderBy(a => a.DueDateTime).ToList();

            return View(sortedAppeals);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                _appeals.Add(appeal);
                return RedirectToAction("Index");
            }
            return View(appeal);
        }

        public IActionResult Delete(int id)
        {
            var appeal = _appeals.Find(a => a.AppealId == id);
            if (appeal == null)
            {
                return NotFound();
            }

            _appeals.RemoveAll(a => a.AppealId == id);
            return RedirectToAction("Index");
        }
    }
}
