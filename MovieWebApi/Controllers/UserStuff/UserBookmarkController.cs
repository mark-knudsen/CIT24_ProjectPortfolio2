using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebApi.Controllers.UserStuff
{
    public class UserBookmarkController : Controller
    {
        // GET: UserBookmarkController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserBookmarkController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserBookmarkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserBookmarkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserBookmarkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserBookmarkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserBookmarkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserBookmarkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
