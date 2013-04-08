using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using GolfGallery;

namespace JsonToDb {
  public class JsonToDbInitializer : DropCreateDatabaseIfModelChanges<GolfContext> {
    //Database.SetInitializer<GolfContext>(new DropCreateDatabaseAlways<GolfContext>());
    protected override void Seed(GolfContext context) {
      base.Seed(context);
    }
  }
}
