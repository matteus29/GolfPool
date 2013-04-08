using System.Collections.Generic;
using System.Linq;
using Golf.Gallery;
using Golf.Data;
using Golf.Web.Mvc.Models;

namespace Golf.Web.Mvc.Models {
  public class UserboardRepository {

    public List<Userboard> GetUserboardsByTournamentId(int tournamentId) {
      var userboards = new List<Userboard>();

      var allUsers = GolfCache.AllUsers();
      var tourny = GolfCache.GetTournamentById(tournamentId);
      var picks = GolfCache.PicksByTournamentId(tourny);
      var allGolferScores = GetGolferScores(tourny, picks);

      foreach (var user in allUsers) {
        if (picks.Any(p => p.UserId == user.Id && p.TournamentId == tournamentId))
          userboards.Add(new Userboard {
            UserScore = CalculateUserScore(allGolferScores.Where(gs => picks.Any(p => p.GolferId == gs.Golfer.Id && p.UserId == user.Id)).ToList(), user, tourny.Par),
            GolferScores = allGolferScores.Where(g => picks.Any(p => p.GolferId == g.Golfer.Id && p.UserId == user.Id)).ToList(),
            Tournament = tourny
          });
      }
      userboards.Sort();
      return userboards;
    }

    private List<GolferScore> GetGolferScores(Tournament tourny, List<Pick> picks) {
      var golferScores = new List<GolferScore>();
      var golfersInField = GolfCache.AllGolfers().Where(g => picks.Where(p => p.TournamentId == tourny.EspnId).Select(p2 => p2.GolferId).Contains(g.Id)).ToList();
      var scores = GolfCache.ScoresByGolfersAndTournament(golfersInField, tourny);

      golfersInField.ForEach(g => {
        var score = scores.First(s => s.GolferEspnId == g.EspnId);
        golferScores.Add(
             new GolferScore {
               Golfer = g,
               Score = score,
               CssClass = scores.First(s => s.GolferEspnId == g.EspnId).Scores.Select(rs => (rs < tourny.Par && rs != 0).ToString()).ToList(),
               RoundsCompleted = GetCompletedRounds(score)
             });
      });
      return golferScores;
    }

    private int GetCompletedRounds(Score score) {
      return score.Scores.Count(s => s > 0);
    }

    private UserScore CalculateUserScore(List<GolferScore> gScores, User user, int par) {
      var scorer = new Scorer();
      
      var uScore = new UserScore { User = user,
                                  TotalToPar = scorer.GetTotalUserScore(gScores.Select(g => g.Score).ToList(), par),
                                  RoundScoresToPar = scorer.GetUserRoundScores(gScores.Select(g => g.Score).ToList(), par)
      };
      uScore.CssClasses = uScore.RoundScoresToPar.Select(rs => (rs < par && rs != 0).ToString()).ToList();
      return uScore;
    }

    private List<List<int>> RotateMatrix(List<List<int>> numNums) {
      var rot = new List<List<int>>();
      for (int i = 0; i < numNums.Count(); i++) {
        rot.Add(numNums.Select(q => q.ToArray()[i]).ToList());
      }
      return rot;
    }

    private List<List<bool>> GetRelativeToPars(List<Score> scores, Tournament tourny) {
      var areUnderPars = new List<List<bool>>();
      scores.ToList().ForEach(s => areUnderPars.Add(s.Scores.Select(r => r < tourny.Par && r != 0).ToList()));

      return areUnderPars;
    }
  }
}