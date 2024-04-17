using Microsoft.AspNetCore.Mvc;
using Appeals.Models;

namespace Appeals.Controllers
{
    public class AppealController : Controller
    {
        public static List<Appeal> _appeals = new List<Appeal>();

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
            _appeals.Remove(appeal);
            return RedirectToAction("Index");
        }
    }
}
