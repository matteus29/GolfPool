using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Golf.Gallery {
  public class UserReader {
    public string FilePath { get; set; }

    public UserReader() { }
    public UserReader(string filePath) {
      FilePath = filePath;
    }

    public List<User> ReadAllUsersFromFile(string fileName) {
      this.FilePath = fileName;
      return ReadAllUsersFromFile();
    }
    public List<User> ReadAllUsersFromFile() {
      var users = new List<User>();

      using (var reader = new StreamReader(FilePath)) {
        users = ReadUsersFromJsonString(reader.ReadToEnd());
        reader.Close();
      }
      return users;
    }

    private List<User> ReadUsersFromJsonString(string jUsers) {
      return JArray.Parse(jUsers).ToObject<List<User>>();
    }
  }
}
