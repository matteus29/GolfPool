using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Golf.Gallery {
  public class GolferReader {
    public string FileName { get; set; }

    public GolferReader() { }
    public GolferReader(string fileName) {
      this.FileName = fileName;
    }

    public List<Golfer> ReadAllGolfersFromFile(string fileName) {
      this.FileName = fileName;
      return ReadAllGolfersFromFile();
    }
    public List<Golfer> ReadAllGolfersFromFile() {
      var golfers = new List<Golfer>();

      using (var reader = new StreamReader(FileName)) {
        golfers = ReadGolfersFromJsonString(reader.ReadToEnd());
        reader.Close();
      }
      return golfers;
    }

    private List<Golfer> ReadGolfersFromJsonString(string jGolfers) {
      return JArray.Parse(jGolfers).ToObject<List<Golfer>>();
    }
  }
}
