using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Golf.Gallery;

namespace Golf.Web.Mvc.Models {
  public class Leaderboard : List<Mapping> {

    public void AddInOrder(Mapping m) {
      base.Add(m);
      this.Sort();
    }
  }
}