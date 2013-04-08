using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Golf.Gallery;

namespace Golf.Web.Mvc.Models {
  public class Userboard : IComparable<Userboard> {
    public UserScore UserScore { get; set; }
    public List<GolferScore> GolferScores { get; set; }
    public Tournament Tournament { get; set; }

    public int CompareTo(Userboard other) {
      var thisCompletedRounds = GetCompletedRounds(this.GolferScores);
      var otherCompletedRounds = GetCompletedRounds(other.GolferScores);

      if (thisCompletedRounds != otherCompletedRounds)
        return otherCompletedRounds.CompareTo(thisCompletedRounds);

      return this.UserScore.TotalToPar.CompareTo(other.UserScore.TotalToPar);
    }

    private int GetCompletedRounds(List<GolferScore> gScores) {
      var roundsByGolfer = gScores.Select(gScore => gScore.Score.Scores.Count(score => score > 0));
      return roundsByGolfer.Where(r => roundsByGolfer.Count(g => g == r) > 1).Max();
    }
  }
}