using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Text;
using GolfGallery;

namespace JsonToDb {
  class Program {
    static void Main(string[] args) {

      using (var db = new GolfContext()) {
        var repo = new GolfRepository();
        GolfRepository.AllGolfers.ForEach(golfer => {
          db.Golfers.Add(golfer);
          Console.WriteLine(string.Format("Added {0}",golfer.FullName));
        });
        db.Tournaments.Add(repo.Tournament);
        repo.Users.ForEach(user => db.Users.Add(user));
        db.SaveChanges();
      }
    }
  }
}
