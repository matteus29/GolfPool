using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Golf.Gallery;

namespace Golf.Web.Mvc.Models {
  public class Mapping : IComparable<Mapping>  {

    public Golfer golfer;
    public User User;
    public Score Score;
    public Tournament Tournament;
    public List<bool> AreUnderPars;

    public Mapping(Golfer golfer, User owner, Score score, Tournament tourny) {
      this.golfer = golfer;
      this.User = owner;
      this.Score = score;
      this.Tournament = tourny;
      if (this.Score != null && this.Tournament != null)
        GetRelativeToPars();
    }

    private void GetRelativeToPars() {
      AreUnderPars = new List<bool>();
      this.Score.Scores.ToList().ForEach(s => AreUnderPars.Add(s < Tournament.Par && s > 0));
    }

    public int CompareTo(Mapping m) {
      return this.Score.ToPar.CompareTo(m.Score.ToPar);
    }
  }
}