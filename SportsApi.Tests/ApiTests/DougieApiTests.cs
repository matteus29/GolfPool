using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using GolfGallery;
using DougieApi;

namespace GolfDraftApp.Tests {
  
  [TestClass]
  public class DougieApiTests {

    [TestMethod]//[Ignore]
    public void CanGetFieldFromTheDougieApi() {
      var dougieApiHandler = new DougieApiHandler();
      var field = dougieApiHandler.GetCurrentField();
      var duplicates = field.GroupBy(p => p).Where(g => g.Count() > 1).ToList();

      //var allGolfers = new GolferReader(@"C:\Users\msimon\Dropbox\SportsApi\Docs\GolferList.golfer").ReadAllGolfersFromFile();
      //var gWriter = new GolferWriter(TestResources.BaseFilePath + @"Real\CurrentField.golfer");
      //gWriter.InsertUniqueGolfers(allGolfers.Where(g => field.Contains(g.Id)).ToList());

      Assert.IsTrue(duplicates.Count == 0);
      Assert.IsInstanceOfType(field, typeof(List<int>));
      Assert.IsTrue(field.Count > 20);
    }

    [TestMethod]
    public void CanGetTournamentNameFromTheDougieApi() {
      var dougieApiHandler = new DougieApiHandler();
      var name = dougieApiHandler.GetTournamentName();

      Assert.IsNotNull(name);
    }

    //[TestMethod][Ignore] //Needs to be during live event
    //public void CanGetLeaderboardFromTheDougieApi() {
    //  var dougieApiHandler = new DougieApiHandler();
    //  var leaderboard = dougieApiHandler.GetLeaderboard();
    //  var duplicates = leaderboard.GroupBy(p => p.Id).Where(g => g.Count() > 1).ToList();

    //  Assert.IsTrue(duplicates.Count == 0);
    //  Assert.IsInstanceOfType(leaderboard, typeof(List<Golfer>));
    //  Assert.IsTrue(leaderboard.Count > 20);
    //}
  }
}