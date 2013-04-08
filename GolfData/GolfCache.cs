using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using Golf.Utilities;
using System.Web.Caching;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Golf.Gallery;
using System.Data.Entity;
using Golf.DougieApi;

namespace Golf.Data {
  public class GolfCache {

    public static List<Golfer> AllGolfers() {
      using (var db = new GolfContext()) {
        return CacheHelper.CacheGet(string.Format("{0}={1}", ConfigurationManager.AppSettings["CacheKey-AllGolfers"], "ALL"), () => {
          return db.Golfers.ToList();},
          null,
          DateTime.Now.AddDays(21),
          Cache.NoSlidingExpiration,
          CacheItemPriority.Default);
      }
    }

    public static List<Pick> PicksByTournamentId(Tournament tourny) {
      using (var db = new GolfContext()) {
        if (db.Picks.Any())
          return db.Picks.Where(p => p.TournamentId == tourny.EspnId).ToList();

        return new List<Pick>();
      }
    }

    public static List<User> AllUsers() {
      using (var db = new GolfContext()) {
        return CacheHelper.CacheGet(string.Format("{0}={1}", ConfigurationManager.AppSettings["CacheKey-UserIds"], "ALL"), () => {
          return db.Users.ToList();},
          null,
          DateTime.Now.AddHours(24),
          Cache.NoSlidingExpiration,
          CacheItemPriority.Default);
      }
    }

    public static List<Tournament> AllTournaments() {
      using (var db = new GolfContext()) {
        return CacheHelper.CacheGet(string.Format("{0}={1}", ConfigurationManager.AppSettings["CacheKey-Tournaments"], "ALL"), () => {
          return db.Tournaments.ToList();
        },
          null,
          DateTime.Now.AddHours(24),
          Cache.NoSlidingExpiration,
          CacheItemPriority.Default);
      }
    }

    public static List<Score> ScoresByGolfersAndTournament(List<Golfer> golfers, Tournament tourny) {
      return GetScoresByGolferIdsAndTournament(golfers.Select(i => i.EspnId).ToList(), tourny);
    }

    public static List<Golfer> FieldByTournament(Tournament tourny) {
      using (var db = new GolfContext()) {
        return CacheHelper.CacheGet(string.Format("{0}={1}", ConfigurationManager.AppSettings["CacheKey-Field"], tourny.EspnId), () => {
          var field = new DougieApi.DougieApiHandler().GetCurrentField(tourny.EspnId);
          return AllGolfers().Where(g => field.Contains(g.EspnId)).ToList();},
          null,
          DateTime.Now.AddMinutes(1),
          Cache.NoSlidingExpiration,
          CacheItemPriority.Default);
      }
    }

    public static Tournament GetLatestTournament() {
      using (var db = new GolfContext()) {
        var dougieApi = new DougieApiHandler();

        var tourny = new Tournament {
          EspnId = dougieApi.GetTournamentId(),
          Name = dougieApi.GetTournamentName(),
          Par = int.Parse(dougieApi.GetTournamentPar())
        };

        if (!db.Tournaments.Any(t => t.EspnId == tourny.EspnId)) {
          db.Tournaments.Add(tourny);
          db.SaveChanges();
          CacheHelper.CacheSet(string.Format("{0}-{1}", ConfigurationManager.AppSettings["CacheKey-Tournaments"], "ALL"), db.Tournaments);
        }
        return tourny;
      }
    }

    public static Tournament GetTournamentById(int id) {
      return new GolfContext().Tournaments.FirstOrDefault(t => t.EspnId == id);
    }

    private static List<Score> GetScoresByGolferIdsAndTournament(List<int> pickedGolferIds, Tournament tourny) {
      var jLeaderboard = JArray.Parse(new DougieApi.DougieApiHandler().GetLeaderboard(tourny.EspnId));
      List<Score> allScores = jLeaderboard.ToObject<List<Score>>();
      var scores = allScores.Where(s => pickedGolferIds.Contains(s.GolferEspnId)).ToList();
      scores.ForEach(sc => {
        sc.ToPar = CalculateRelationToPar(sc, tourny.Par);
        sc.TournamentId = tourny.EspnId;
      });
      //using (var db = new GolfContext()) {
      //  db.Scores.Concat(scores);
      //  db.SaveChanges();
      //}
      return scores;
    }

    private static int CalculateRelationToPar(Score score, int tournamentPar) {
      return score.Scores.Sum() - tournamentPar * score.Scores.Count(s => s != 0);
    }
  }
}
