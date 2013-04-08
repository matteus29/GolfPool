using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GolfGallery;

namespace GolfDraftApp.Tests {
  
  [TestClass]
  public class ScoringTests {

    private GolfRepository _golfRepo;
    private TestResources _testResources;

    //[TestInitialize]
    //public void Init() {
    //  _testResources = new TestResources();
    //  _golfRepo = new GolfRepository(defaultConfig: false);
    //  _golfRepo.Add(_testResources.UserWithFourGolfers);
    //  _golfRepo.Add(_testResources.Golfers);
    //}

    //[TestMethod]
    //public void CanCalculateUserScoreForSingleRound() {
    //  var roundOneUserScore = _golfRepo.GetUserScore(_testResources.UserWithFourGolfers.Id, 1);

    //  Assert.AreEqual(138, roundOneUserScore);
    //}

    //[TestMethod]
    //public void CanCalculateUserTwoRoundScore() {
    //  _golfRepo.Golfers.ForEach(g => g.Scores[3] = 0);
    //  _golfRepo.Golfers.ForEach(g => g.Scores[2] = 0);

    //  var totalUserScore = _golfRepo.GetUserScore(_testResources.UserWithFourGolfers.Id);

    //  Assert.AreEqual(281, totalUserScore);
    //}

    //[TestMethod]
    //public void CanCalculateUserThreeRoundScore() {
    //  _golfRepo.Golfers.ForEach(g => g.Scores[3] = 0);

    //  var totalUserScore = _golfRepo.GetUserScore(_testResources.UserWithFourGolfers.Id);

    //  Assert.AreEqual(412, totalUserScore);
    //}

    //[TestMethod]
    //public void CanCalculateUserFourRoundScore() {
    //  var totalUserScore = _golfRepo.GetUserScore(_testResources.UserWithFourGolfers.Id);

    //  Assert.AreEqual(543, totalUserScore);
    //}
  }
}
