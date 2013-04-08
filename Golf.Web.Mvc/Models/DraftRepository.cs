using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Golf.Gallery;
using Golf.DougieApi;
using Golf.Utilities;
using System.Configuration;
using Golf.Data;

namespace Golf.Web.Mvc.Models {
  public class DraftRepository {

    public Draft GetDraft(int tEspnId) {
      
      Tournament currentTourny;
      if (tEspnId == 0)
        currentTourny = GolfCache.GetLatestTournament();
      else {
        currentTourny = GolfCache.GetTournamentById(tEspnId);
        if (currentTourny == null)
          currentTourny = GolfCache.GetLatestTournament();
      }

      var draft = new Draft(currentTourny);

      var field = GolfCache.FieldByTournament(currentTourny);
      var picks = GolfCache.PicksByTournamentId(currentTourny);
      var users = GolfCache.AllUsers();

      draft.AvailableGolfers = field.Where(f => !picks.Select(p => p.GolferId).Contains(f.Id)).ToList();
      picks.ForEach(p => draft.DraftPicks.Add(new Mapping(field.First(g => g.Id == p.GolferId), users.First(u => u.Id == p.UserId), null, currentTourny)));        
      draft.Users = users;

      return draft;
    }

    public static List<Pick> GetDraftPicks(GolfContext db, int tEspnId) {
      if (db.Picks.Any())
        return db.Picks.Where(p => p.TournamentId == tEspnId).ToList();
      return new List<Pick>();
    }

    public static void RemoveDraftPicks(int[] picks_to_remove, int tournamentId) {
      using (var db = new GolfContext()) {
        var tourny = GolfCache.GetTournamentById(tournamentId);

        foreach (var pick in picks_to_remove) {
          var d = db.Picks.First(p => p.GolferId == pick && p.TournamentId == tourny.EspnId);
          db.Picks.Remove(d);
        }
        db.SaveChanges();
        CacheDraftPicks(db,tourny.EspnId);
      }

    }

    public static void SaveDraftPick(int userId, int golferId, int tEspnId) {
      using (var db = new GolfContext()) {
        var tourny = GolfCache.GetTournamentById(tEspnId);
        var picks = DraftRepository.GetDraftPicks(db, tourny.EspnId);

        var newPick = new Pick(userId, golferId, tourny.EspnId);

        if (!picks.Any(p => p.GolferId == newPick.GolferId && p.TournamentId == newPick.TournamentId)) {
          db.Picks.Add(newPick);
          db.SaveChanges();
          picks.Add(newPick);
          DraftRepository.CacheDraftPicks(db, tourny.EspnId);
        }
      }
    }

    public static void CacheDraftPicks(GolfContext db, int tournamentId) {
      CacheHelper.CacheSet(string.Format("{0}-{1}", ConfigurationManager.AppSettings["CacheKey-DraftedPlayersIds"], tournamentId), GetDraftPicks(db, tournamentId));
    }
  }
}