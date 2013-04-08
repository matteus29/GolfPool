using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Golf.Gallery;
using Golf.Utilities;
using Golf.Data;
using System.Configuration;

namespace Golf.Web.Mvc.Models {
  public class LeaderboardRepository {

    public Leaderboard GetLeaderboard(int tournamentId) {
      Tournament tourny;
      if (tournamentId == 0)
        tourny = GolfCache.GetLatestTournament();
      else
        tourny = GolfCache.GetTournamentById(tournamentId);

      if (tourny == null)
        tourny = GolfCache.GetLatestTournament();

      var field = GolfCache.FieldByTournament(tourny);
      var picks = GolfCache.PicksByTournamentId(tourny);
      var users = GolfCache.AllUsers();

      var pickedPlayers = field.Where(a => picks.Any(p => p.GolferId == a.Id)).ToList();

      var scores = GolfCache.ScoresByGolfersAndTournament(pickedPlayers, tourny);

      var currentLeaderboard = new Leaderboard();
      pickedPlayers.ForEach(pp => currentLeaderboard.AddInOrder(new Mapping(pp, 
                                                      users.First(u => u.Id == picks.First(p => p.GolferId == pp.Id).UserId),
                                                      scores.First(s => pp.EspnId == s.GolferEspnId),
                                                      tourny)
                                                      )
                            );
      return currentLeaderboard;
    }
  }
}