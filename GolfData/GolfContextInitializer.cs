using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Golf.DougieApi;
using Golf.Gallery;

namespace Golf.Data {
  public class GolfContextInitializer : DropCreateDatabaseIfModelChanges<GolfContext> {

    protected override void Seed(GolfContext context) {
      var repo = new GolfRepository();

      var golferIds = context.Golfers.Select(g => g.EspnId).ToList();
      GolfRepository.AllGolfers.ForEach(golfer => {
        if (!golferIds.Contains(golfer.EspnId))
          context.Golfers.Add(golfer);
      });

      var userIds = context.Users.Select(x => x.Id).ToList();
      repo.Users.ForEach(user => {
        if (!userIds.Contains(user.Id))
          context.Users.Add(user);
      });

      context.SaveChanges();
    }
  }
}