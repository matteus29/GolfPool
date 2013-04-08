using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolfGallery;

namespace GolfDraftApp.Tests {

  [TestClass]
  public class DrafterTests {

    [TestMethod]
    public void CanReadLatestDraftFile() {
      var directory = TestResources.BaseFilePath + @"Drafter";
      var drafter = new Drafter(directory);

      var latestDraft = drafter.GetLatestDraft();

      Assert.AreEqual(16, latestDraft.Picks.Count);
    }

    [TestMethod]
    public void CanReadLatestFieldFile() {
      var directory = TestResources.BaseFilePath + @"Drafter";
      var drafter = new Drafter(directory);

      var latestField = drafter.GetLatestField();

      Assert.AreEqual(146, latestField.Count);
    }
  }
}
