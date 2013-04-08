using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolfGallery;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GolfDraftApp.Tests {
  
  [TestClass]
  public class UserCacheTests {

    private List<User> _users;

    [TestInitialize]
    public void Init() {
      _users = new TestResources().Users;
    }

    [TestMethod]
    public void CanReadSingleUserFromFile() {
      UserReader uReader = new UserReader(TestResources.BaseFilePath + "readSingleUserTest.golfuser");
      List<User> users = uReader.ReadAllUsersFromFile();

      Assert.AreEqual(1, users.Count);
      Assert.AreEqual(1, users[0].Id);
      Assert.AreEqual("Arnie Palmer", users[0].Name);
    }

    [TestMethod]
    public void CanReadTwoUsersFromFile() {
      UserReader uReader = new UserReader(TestResources.BaseFilePath + "readTwoUsersTest.golfuser");
      List<User> users = uReader.ReadAllUsersFromFile();

      Assert.AreEqual(2, users.Count);
      Assert.AreEqual(1, users[0].Id);
      Assert.AreEqual("Arnie Palmer", users[0].Name);
      Assert.AreEqual(2, users[1].Id);
      Assert.AreEqual("Bilbo Baggins", users[1].Name);

    }

    [TestMethod]
    public void CanWriteSingleUserToFile() {
      var newFile = TestResources.BaseFilePath + "writeSingleUserTest.golfuser";

      var uWriter = new UserWriter(newFile);
      uWriter.InsertUsers(_users[0]);

      var uReader = new UserReader(newFile);
      var users = uReader.ReadAllUsersFromFile();

      File.Delete(newFile);

      Assert.AreEqual(1, users.Count());
      Assert.AreEqual(_users[0].Id, users[0].Id);
      Assert.AreEqual(_users[0].Name, users[0].Name);
      Assert.AreEqual(_users[0].Golfers.Count, users[0].Golfers.Count);
    }

    [TestMethod]
    public void CanWriteTwoUsersToFile() {
      var newFile = TestResources.BaseFilePath + "writeTwoUsersTest.golfuser";

      var uWriter = new UserWriter(newFile);
      uWriter.InsertUsers(_users);

      var uReader = new UserReader(newFile);
      var users = uReader.ReadAllUsersFromFile();

      File.Delete(newFile);

      Assert.AreEqual(2, users.Count());
      Assert.AreEqual(_users[0].Id, users[0].Id);
      Assert.AreEqual(_users[0].Name, users[0].Name);
      Assert.AreEqual(_users[0].Golfers.Count, users[0].Golfers.Count);
      Assert.AreEqual(_users[1].Id, users[1].Id);
      Assert.AreEqual(_users[1].Name, users[1].Name);
      Assert.AreEqual(_users[1].Golfers.Count, users[1].Golfers.Count);
    }

    [TestMethod]
    public void CanWriteSingleUserToExistingFile() {
      var existingFile = TestResources.BaseFilePath + "writeUserToExistingFileTest.golfuser";

      var uReader = new UserReader(existingFile);
      var beforeAddedUsers = uReader.ReadAllUsersFromFile();

      var uWriter = new UserWriter(existingFile);
      uWriter.InsertUsers(_users[0]);

      var afterAddedUsers = uReader.ReadAllUsersFromFile();

      Assert.AreEqual(beforeAddedUsers.Count + 1, afterAddedUsers.Count);
    }

    [TestMethod]
    public void CanWriteMultipleUsersToExistingFile() {
      var existingFile = TestResources.BaseFilePath + "writeUserToExistingFileTest.golfuser";

      var uReader = new UserReader(existingFile);
      var beforeAddedUsers = uReader.ReadAllUsersFromFile();

      var uWriter = new UserWriter(existingFile);
      uWriter.InsertUsers(_users);

      var afterAddedUsers = uReader.ReadAllUsersFromFile();

      Assert.AreEqual(beforeAddedUsers.Count + _users.Count, afterAddedUsers.Count);
    }

    [TestMethod]
    public void WillOnlyInsertUniqueUsersIntoFile() {
      var newFile = TestResources.BaseFilePath + "uniqueInsertToFileTest.golfer";

      var uWriter = new UserWriter(newFile);
      uWriter.InsertUsers(_users[0]);

      var uReader = new UserReader(newFile);
      var beforeAddedGolfers = uReader.ReadAllUsersFromFile();

      uWriter.InsertUniqueUsers(_users);

      var afterAddedGolfers = uReader.ReadAllUsersFromFile();

      File.Delete(newFile);

      Assert.AreEqual(1, beforeAddedGolfers.Count);
      Assert.AreEqual(_users.Count, afterAddedGolfers.Count);
    }
  }
}
