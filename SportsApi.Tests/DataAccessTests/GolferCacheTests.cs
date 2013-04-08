using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolfGallery;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GolfDraftApp.Tests {

  [TestClass]
  public class GolferCacheTests {

    private List<Golfer> _golfers;

    [TestInitialize]
    public void Init() {
      _golfers = new TestResources().Golfers;
    }

    [TestMethod]
    public void CanReadSingleGolferFromFile() {
      var gReader = new GolferReader(TestResources.BaseFilePath + "readSingleGolferTest.golfer");
      var actualGolfer = gReader.ReadAllGolfersFromFile().ToList()[0];

      Assert.AreEqual(_golfers[0].Id, actualGolfer.Id);
      Assert.AreEqual(_golfers[0].FirstName, actualGolfer.FirstName);
      Assert.AreEqual(_golfers[0].LastName, actualGolfer.LastName);
      Assert.AreEqual(_golfers[0].FullName, actualGolfer.FullName);
      Assert.AreEqual(_golfers[0].DisplayName, actualGolfer.DisplayName);
      Assert.AreEqual(_golfers[0].ShortName, actualGolfer.ShortName);
    }

    [TestMethod]
    public void CanReadThreeGolfersFromFile() {
      var gReader = new GolferReader(TestResources.BaseFilePath + "readThreeGolfersTest.golfer");
      var golfers = gReader.ReadAllGolfersFromFile().ToList();

      Assert.AreEqual(3, golfers.Count);
      Assert.AreEqual(_golfers[0].FirstName, golfers[0].FirstName);
      Assert.AreEqual(_golfers[1].FirstName, golfers[1].FirstName);
      Assert.AreEqual(_golfers[2].FirstName, golfers[2].FirstName);
    }

    [TestMethod]
    public void CanWriteSingleGolferToFile() {
      var newFile = TestResources.BaseFilePath + "writeSingleGolferTest.golfer";

      var gWriter = new GolferWriter(newFile);
      gWriter.InsertGolfers(_golfers[0]);

      var gReader = new GolferReader(newFile);
      var golfers = gReader.ReadAllGolfersFromFile();

      File.Delete(newFile);

      Assert.AreEqual(1, golfers.Count());
      Assert.AreEqual(_golfers[0].Id, golfers[0].Id);
      Assert.AreEqual(_golfers[0].FirstName, golfers[0].FirstName);
      Assert.AreEqual(_golfers[0].LastName, golfers[0].LastName);
      Assert.AreEqual(_golfers[0].FullName, golfers[0].FullName);
      Assert.AreEqual(_golfers[0].DisplayName, golfers[0].DisplayName);
      Assert.AreEqual(_golfers[0].ShortName, golfers[0].ShortName);
    }

    [TestMethod]
    public void CanWriteThreeGolfersToFile() {
      var newFile = TestResources.BaseFilePath + "writeMultipleGolfersTest.golfer";

      var gWriter = new GolferWriter(newFile);
      gWriter.InsertGolfers(_golfers);

      var gReader = new GolferReader(newFile);
      var golfers = gReader.ReadAllGolfersFromFile();

      File.Delete(newFile);

      Assert.AreEqual(_golfers.Count, golfers.Count);
      Assert.AreEqual(_golfers[0].Id, golfers[0].Id);
      Assert.AreEqual(_golfers[1].Id, golfers[1].Id);
      Assert.AreEqual(_golfers[2].Id, golfers[2].Id);
    }

    [TestMethod]
    public void CanWriteSingleGolferToExistingFile() {
      var existingFile = TestResources.BaseFilePath + "writeGolferToExistingFileTest.golfer";

      var gReader = new GolferReader(existingFile);
      var beforeAddedGolfers = gReader.ReadAllGolfersFromFile();

      var gWriter = new GolferWriter(existingFile);
      gWriter.InsertGolfers(_golfers[3]);

      var afterAddedGolfers = gReader.ReadAllGolfersFromFile();

      Assert.AreEqual(beforeAddedGolfers.Count + 1, afterAddedGolfers.Count);
    }

    [TestMethod]
    public void CanWriteMultipleGolfersToExistingFile() {
      var existingFile = TestResources.BaseFilePath + "writeGolferToExistingFileTest.golfer";

      var gReader = new GolferReader(existingFile);
      var beforeAddedGolfers = gReader.ReadAllGolfersFromFile();

      var gWriter = new GolferWriter(existingFile);
      gWriter.InsertGolfers(_golfers);

      var afterAddedGolfers = gReader.ReadAllGolfersFromFile();

      Assert.AreEqual(beforeAddedGolfers.Count + _golfers.Count, afterAddedGolfers.Count);
    }

    [TestMethod]
    public void WillOnlyInsertUniqueGolfersIntoFile() {
      var newFile = TestResources.BaseFilePath + "uniqueInsertToFileTest.golfer";

      var gWriter = new GolferWriter(newFile);
      gWriter.InsertGolfers(_golfers.Take(2).ToList());

      var gReader = new GolferReader(newFile);
      var beforeAddedGolfers = gReader.ReadAllGolfersFromFile();

      gWriter.InsertUniqueGolfers(_golfers);

      var afterAddedGolfers = gReader.ReadAllGolfersFromFile();

      File.Delete(newFile);

      Assert.AreEqual(2, beforeAddedGolfers.Count);
      Assert.AreEqual(_golfers.Count, afterAddedGolfers.Count);
    }
  }
}
