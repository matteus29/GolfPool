using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Golf.Web.Mvc.Models;
using Golf.Gallery;

namespace Golf.Web.Mvc.Controllers {
  public class UserboardController : Controller {

    public ActionResult Index(int id) {
      return View(new UserboardRepository().GetUserboardsByTournamentId(id));
    }
  }
}
