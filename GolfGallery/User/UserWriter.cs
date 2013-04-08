using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Golf.Gallery {
  public class UserWriter {
    public string FilePath { get; set; }

    public UserWriter(string filePath) {
      FilePath = filePath;
    }

    public void InsertUsers(User user) {
      InsertUsers(new List<User> { user });
    }

    public void InsertUsers(List<User> users) {
      if (File.Exists(FilePath)) {
        var existingUsers = new UserReader(FilePath).ReadAllUsersFromFile();
        users = users.Concat(existingUsers).ToList();
      }

      WriteUsersToFile(users);
    }

    public void InsertUniqueUsers(List<User> users) {
      var uReader = new UserReader(FilePath);
      var existingUsers = new List<User>();
      try {
        existingUsers = uReader.ReadAllUsersFromFile();

        users.Where(u => !existingUsers.Any(e => e.Id == u.Id)).ToList().
          ForEach(uniqueUser => existingUsers.Add(uniqueUser));

        WriteUsersToFile(existingUsers);
      }
      catch {
        WriteUsersToFile(users);
      }
    }

    private void WriteUsersToFile(List<User> users) {
      using (StreamWriter writer = new StreamWriter(FilePath)) {
        var jSerializer = new JsonSerializer();
        jSerializer.Serialize(writer, users);
        writer.Close();
      }
    }
  }
}
