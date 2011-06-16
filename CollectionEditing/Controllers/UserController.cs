using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectionEditing.Models;

namespace CollectionEditing.Controllers
{
    public class UserController : Controller
    {
        private static User _currentUser;

        private static User CurrentUser {
            get {
                if (_currentUser == null)
                    _currentUser = GetFakeUser();
                return _currentUser;
            } set {
                _currentUser = value;
            }
        }

        private static User GetFakeUser()
        {
            return new User() {
                Id = 1,
                Name = "Ivan Zlatev",
                FavouriteMovies = new List<Movie>() {
                    new Movie() { Title = "Movie 1", Rating = 5 },
                    new Movie() { Title = "Movie 2", Rating = 10 },
                    new Movie() { Title = "Movie 3", Rating = 12 }
                }
            };
        }

        public ActionResult Reset()
        {
            CurrentUser = GetFakeUser();
            return RedirectToAction("Display");
        }

        public ActionResult Edit()
        {
            return View(CurrentUser);
        }

        public ActionResult MovieEntryRow()
        {
            return PartialView("MovieEntryEditor");
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (!this.ModelState.IsValid)
                return View(user);

            CurrentUser = user;
            return RedirectToAction("Display");
        }

        public ActionResult Display()
        {
            return View(CurrentUser);
        }
    }
}
