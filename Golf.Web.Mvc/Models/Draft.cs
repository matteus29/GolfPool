using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Golf.Gallery;

namespace Golf.Web.Mvc.Models {
  public class Draft {
    public List<User> Users { get; set; }
    public List<Golfer> AvailableGolfers { get; set; }
    public List<Mapping> DraftPicks { get; set; }
    public Tournament Tournament { get; set; }

    public Draft(Tournament tourny) {
      Users = new List<User>();
      AvailableGolfers = new List<Golfer>();
      DraftPicks = new List<Mapping>();
      Tournament = tourny;
    }
  }
}