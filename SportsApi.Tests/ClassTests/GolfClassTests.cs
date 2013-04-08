using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolfGallery;

namespace GolfDraftApp.Tests {
  
  [TestClass]
  public class GolfClassTests {

    private Draft _draft;

    [TestInitialize]
    public void Init() {
      _draft = new TestResources().DraftFakeInfo;
    }

    [TestMethod]
    public void DraftHasPickEquality() {
      Assert.IsTrue(_draft.Has(new Pick(1, 4444)));
      Assert.IsTrue(_draft.Has(new Pick(2, 2222)));
      Assert.IsTrue(_draft.Has(1, 1111));
      Assert.IsTrue(_draft.Has(3, 6666));
    }

    [TestMethod]
    public void DraftHasPickInequality() {
      Assert.IsFalse(_draft.Has(new Pick(1, 2222)));
      Assert.IsFalse(_draft.Has(new Pick(2, 0)));
      Assert.IsFalse(_draft.Has(1, 5555));
      Assert.IsFalse(_draft.Has(3, 2222));
    }
  }
}
