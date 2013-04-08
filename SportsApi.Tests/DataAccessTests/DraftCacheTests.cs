using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolfGallery;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GolfDraftApp.Tests {
  
  [TestClass]
  public class DraftCacheTests {

    private Draft _draft;

    [TestInitialize]
    public void Init() {
      _draft = new TestResources().Draft1;
    }

    [TestMethod]
    public void CanReadDraftFromFile() {
      var dReader = new DraftReader(TestResources.BaseFilePath + "draft1.golfdraft");
      var draft = dReader.ReadDraftFromFile();

      Assert.AreEqual("MRI Duffer's Classic", draft.TournamentName);
      Assert.AreEqual(1, draft.Picks[0].UserId);
      Assert.AreEqual(1229, draft.Picks[0].GolferId);
      Assert.AreEqual(2, draft.Picks[1].UserId);
      Assert.AreEqual(1337, draft.Picks[1].GolferId);
      Assert.AreEqual(1, draft.Picks[2].UserId);
      Assert.AreEqual(7777, draft.Picks[2].GolferId);
      Assert.AreEqual(2, draft.Picks[3].UserId);
      Assert.AreEqual(9975, draft.Picks[3].GolferId);
    }

    [TestMethod]
    public void CanWriteDraftToFile() {
      var dWriter = new DraftWriter(TestResources.BaseFilePath, "writeDraftTest.golfdraft");
      dWriter.WriteDraftToFile(_draft);

      var dReader = new DraftReader(TestResources.BaseFilePath + "writeDraftTest.golfdraft");
      var draft = dReader.ReadDraftFromFile();

      File.Delete(TestResources.BaseFilePath + "writeDraftTest.golfdraft");

      Assert.AreEqual("MRI Duffer's Classic", draft.TournamentName);
      Assert.IsTrue(draft.Has(_draft.Picks[0]));
      Assert.IsTrue(draft.Has(_draft.Picks[1]));
      Assert.IsTrue(draft.Has(_draft.Picks[2]));
      Assert.IsTrue(draft.Has(_draft.Picks[3]));
    }

    [TestMethod]
    public void CanAddPickInDraftToExistingFile() {
      var dWriter = new DraftWriter(TestResources.BaseFilePath + "writePickToExistingDraft.golfdraft");
      dWriter.WriteDraftToFile(_draft);

      var dReader = new DraftReader(TestResources.BaseFilePath + "writePickToExistingDraft.golfdraft");
      var prePickDraft = dReader.ReadDraftFromFile();

      var newPick = new Pick(3, 9999);
      var feedback = dWriter.AddPickToFile(newPick);

      var postPickDraft = dReader.ReadDraftFromFile();

      File.Delete(TestResources.BaseFilePath + "writePickToExistingDraft.golfdraft");

      Assert.IsTrue(feedback);
      Assert.AreEqual(prePickDraft.Picks.Count + 1, postPickDraft.Picks.Count);
    }

    [TestMethod]
    public void CanNotAddPickIfExistsInDraft() {
      var dWriter = new DraftWriter(TestResources.BaseFilePath + "writePickToExistingDraft.golfdraft");
      dWriter.WriteDraftToFile(_draft);

      var dReader = new DraftReader(TestResources.BaseFilePath + "writePickToExistingDraft.golfdraft");
      var prePickDraft = dReader.ReadDraftFromFile();

      var newPick = new Pick(1, 1229);
      var feedback = dWriter.AddPickToFile(newPick);

      var postPickDraft = dReader.ReadDraftFromFile();

      File.Delete(TestResources.BaseFilePath + "writePickToExistingDraft.golfdraft");

      Assert.IsFalse(feedback);
      Assert.AreEqual(prePickDraft.Picks.Count, postPickDraft.Picks.Count);
    }
  }
}
