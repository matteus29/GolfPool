using System.Collections.Generic;
using Golf.Web.Mvc.Models;
using Golf.Gallery;

namespace Golf.Tests.Resources {
  public static class Userboards {

    public static Userboard ThreeDone = new Userboard {
      GolferScores = new List<GolferScore> {
        new GolferScore {
          Score = TestScore(70,3)
        },
        new GolferScore {
          Score = TestScore(70,3)
        },
        new GolferScore {
          Score = TestScore(70,3)
        },
        new GolferScore {
          Score = TestScore(70,3)
        }
      }
    };

    private static Score TestScore(int score, int rounds) {
      var scores = new List<int>();

      for (int i = 0; i < 4; i++) {
        if (i < rounds)
          scores.Add(score);
        else
          scores.Add(0);
      }

      return new Score {
        Scores = scores.ToArray()
      };
    }
  }
}
