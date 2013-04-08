using System.Collections.Generic;
using Golf.Gallery;

namespace Golf.Web.Mvc.Models {
  public class UserScore {
    public User User { get; set; }
    public int TotalToPar { get; set; }
    public List<int> RoundScoresToPar { get; set; }
    public List<string> CssClasses { get; set; }
  }
}