using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Golf.Gallery;
using Golf.Web.Mvc.Models;
using Golf.Utilities;
using System.Configuration;
using System.Data.Objects;

namespace Golf.Web.Mvc.Controllers {
  public class DraftController : Controller {

    public ActionResult Index(int id = 0) {
      return View("Index", new DraftRepository().GetDraft(id));
    }

    [HttpPost]
    public ActionResult Pick(int userId, int golferId, int tEspnId) {
      DraftRepository.SaveDraftPick(userId, golferId, tEspnId);

      return this.Index(tEspnId);
    }

    [HttpPost]
    public ActionResult Delete(int[] picks_to_remove, int tEspnId) {
      if (picks_to_remove != null)
        DraftRepository.RemoveDraftPicks(picks_to_remove, tEspnId);
      return this.Index(tEspnId);
    }
  }
}
