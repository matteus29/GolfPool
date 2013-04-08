using System.Collections.Generic;
using System.Linq;

namespace Golf.Gallery {
  public class Pick {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GolferId { get; set; }
    public int TournamentId { get; set; }

    public Pick() {}
    public Pick(int userId, int golferId, int tournamentId) {
      this.UserId = userId;
      this.GolferId = golferId;
      this.TournamentId = tournamentId;
    }
  }
}
