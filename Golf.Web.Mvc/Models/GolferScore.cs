using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Golf.Gallery;

namespace Golf.Web.Mvc.Models {
  public class GolferScore {
    public Golfer Golfer { get; set; }
    public Score Score { get; set; }
    public List<string> CssClass { get; set; }
    public int RoundsCompleted { get; set; }
  }
}