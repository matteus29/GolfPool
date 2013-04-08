using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Golf.Gallery;
using Golf.Tests.Resources;

namespace Golf.Tests {

  [TestClass]
  public class ScoringTests {
    private User user = TestData.User;
    private List<Golfer> golfers = TestData.Golfers.Take(4).ToList();
    private Tournament tourny = TestData.Tourny;
    private List<Pick> picks = TestData.Picks;

    private Scorer scorer;
    
    [TestInitialize]
    public void Setup() {
      scorer = new Scorer();
    }

    [TestMethod]
    public void Thru_1_Round() {      
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {70,0,0,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {71,0,0,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {72,0,0,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {73,0,0,0}
        },
      };

      Assert.AreEqual(-3, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-3, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
    }

    [TestMethod]
    public void Thru_2_Rounds() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {70,66,0,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {71,75,0,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {72,66,0,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {73,66,0,0}
        },
      };


      Assert.AreEqual(-15, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-3, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
      Assert.AreEqual(-12, scorer.GetUserRoundScores(scores, tourny.Par)[1]);
    }

    [TestMethod]
    public void Thru_3_Rounds() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {70,66,70,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {71,75,69,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {72,66,69,0}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {73,66,68,0}
        },
      };

      Assert.AreEqual(-22, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-3, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
      Assert.AreEqual(-12, scorer.GetUserRoundScores(scores, tourny.Par)[1]);
      Assert.AreEqual(-7, scorer.GetUserRoundScores(scores, tourny.Par)[2]);
    }

    [TestMethod]
    public void Thru_4_Rounds() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {70,66,70,72}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {71,75,69,72}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {72,66,69,72}
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {73,66,68,72}
        },
      };

      Assert.AreEqual(-22, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-3, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
      Assert.AreEqual(-12, scorer.GetUserRoundScores(scores, tourny.Par)[1]);
      Assert.AreEqual(-7, scorer.GetUserRoundScores(scores, tourny.Par)[2]);
      Assert.AreEqual(0, scorer.GetUserRoundScores(scores, tourny.Par)[3]);
    }

    [TestMethod]
    public void No_Golfers_Done() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
      };

      Assert.AreEqual(0, scorer.GetTotalUserScore(scores, tourny.Par));
    }

    [TestMethod]
    public void One_Golfer_Done() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {69,0,0,0},
          RoundProgress = "F",
          RoundToPar = -3
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
      };

      Assert.AreEqual(-3, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-3, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
    }

    [TestMethod]
    public void Two_Golfers_Done() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {69,0,0,0},
          RoundProgress = "F",
          RoundToPar = -3
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {63,0,0,0},
          RoundProgress = "F",
          RoundToPar = -9
        },
      };

      Assert.AreEqual(-12, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-12, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
    }

    [TestMethod][Ignore]
    public void One_Golfer_Partial_Thru_Round() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "9:30 PM",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {0,0,0,0},
          RoundProgress = "5",
          RoundToPar = -1
        },
      };

      Assert.AreEqual(-1, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-1, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
    }

    [TestMethod][Ignore]
    public void Three_Golfers_Miss_Cut() {
      var scores = new List<Score> {
        new Score {
          TournamentId=1,
          GolferEspnId = 99,
          Scores = new int[] {74,77,0,0},
          RoundProgress = "MC",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 88,
          Scores = new int[] {70,71,66,65},
          RoundProgress = "F",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 77,
          Scores = new int[] {71,74,0,0},
          RoundProgress = "MC",
          RoundToPar = 0
        },
        new Score {
          TournamentId=1,
          GolferEspnId = 66,
          Scores = new int[] {74,75,0,0},
          RoundProgress = "MC",
          RoundToPar = 0
        },
      };

      Assert.AreEqual(null, scorer.GetTotalUserScore(scores, tourny.Par));
      Assert.AreEqual(-3, scorer.GetUserRoundScores(scores, tourny.Par)[0]);
      Assert.AreEqual(-2, scorer.GetUserRoundScores(scores, tourny.Par)[1]);
      Assert.AreEqual(null, scorer.GetUserRoundScores(scores, tourny.Par)[2]);
      Assert.AreEqual(null, scorer.GetUserRoundScores(scores, tourny.Par)[3]);

    }
  }
}
