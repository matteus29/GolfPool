using System.Collections.Generic;
using System.Linq;

namespace Golf.Gallery {
  public class Scorer {

    public int GetTotalUserScore(List<Score> scores, int par) {
      return GetUserRoundScores(scores, par).Sum();
    }

    public List<int> GetUserRoundScores(List<Score> scores, int par) {
      var roundByRoundScores = RotateMatrix(scores.Select(s => s.Scores.ToList()).ToList());

      var roundScores = new List<int>();
      foreach(var rs in roundByRoundScores) {
        var completedRounds = rs.Where(r => r > 50);
        if(completedRounds.Count() > 1)
          roundScores.Add(completedRounds.OrderBy(g => g).Take(2).Sum() - (2 * par));
        else if (completedRounds.Count() == 1) {
          roundScores.Add(completedRounds.Sum() + rs.Min() - par);
        }
        else {
          roundScores.Add(rs.OrderBy(g => g).Take(2).Sum());
        }
      }

      return roundScores;
    }

    private List<List<int>> RotateMatrix(List<List<int>> numNums) {
      var rot = new List<List<int>>();
      for (int i = 0; i < numNums.Count(); i++) {
        rot.Add(numNums.Select(q => q.ToArray()[i]).ToList());
      }
      return rot;
    }
  }
}
