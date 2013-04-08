using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Golf.Gallery;
using Golf.Web.Mvc.Models;


namespace Golf.Web.Mvc.Controllers {
  public class LeaderboardController : Controller {
 
    public ActionResult Index(int id = 0) {
      return View(new LeaderboardRepository().GetLeaderboard(id));
    }

  }
}
