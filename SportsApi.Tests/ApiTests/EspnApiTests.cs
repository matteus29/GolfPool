using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolfGallery;
using EspnApi;
using System.Collections.Generic;
using System.Linq;

namespace GolfDraftApp.Tests {

  [TestClass]
  public class EspnApiTests {

    [TestMethod]
    public void CanReceive1GolferFromEspnAndGolferIsFilledOut() {
      var espnHandler = new EspnManHandler();
      var golfers = espnHandler.GetGolfersFromEspn(0,1);
      Assert.AreEqual(1, golfers.Count);
      Assert.IsNotNull(golfers[0].DisplayName);
      Assert.IsNotNull(golfers[0].FirstName);
      Assert.IsNotNull(golfers[0].LastName);
      Assert.IsNotNull(golfers[0].Link);
      Assert.IsNotNull(golfers[0].ShortName);
      Assert.IsNotNull(golfers[0].FullName);
      Assert.IsNotNull(golfers[0].Id);
    }

    [TestMethod][Ignore]
    public void SaveAllGolfersFromEspn() {
      var espnHandler = new EspnManHandler();
      var gWriter = new GolferWriter(TestResources.BaseFilePath + "GolferList.golfer");

      for (int i = 900; i >= 0; i -= 20) {
        var golfers = espnHandler.GetGolfersFromEspn(i,100);
        if (golfers.Any(k => k.Id == 5548 || k.LastName == "Hadwin")) {
          var x = "";
        }
        gWriter.InsertUniqueGolfers(golfers);
        System.Threading.Thread.Sleep(1000);
      }
      var golfersFromFile = new GolferReader(TestResources.BaseFilePath + "GolferList.golfer").ReadAllGolfersFromFile();

      Assert.IsTrue(golfersFromFile.Count > 900);
    }
  }
}
