using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Golf.Gallery {
  public class GolferWriter {
    public string FilePath { get; set; }

    public GolferWriter(string filePath) {
      FilePath = filePath;
    }

    public void InsertGolfers(Golfer golfer) {
      InsertGolfers(new List<Golfer>{golfer});
    }

    public void InsertGolfers(List<Golfer> golfers) {
      if (File.Exists(FilePath)) {
        var existingGolfers = new GolferReader(FilePath).ReadAllGolfersFromFile();
        golfers = golfers.Concat(existingGolfers).ToList();
      }

      WriteGolfersToFile(golfers);
    }

    public void InsertUniqueGolfers(List<Golfer> golfers) {
      var gReader = new GolferReader(FilePath);
      var existingGolfers = new List<Golfer>();
      try {
        existingGolfers = gReader.ReadAllGolfersFromFile();

        golfers.Where(g => !existingGolfers.Any(h => h.FullName == g.FullName)).ToList().
          ForEach(uniqueGolfer => existingGolfers.Add(uniqueGolfer));
        
        WriteGolfersToFile(existingGolfers);
      }
      catch{
        WriteGolfersToFile(golfers);
      }
    }

    private void WriteGolfersToFile(List<Golfer> golfers) {
      using (StreamWriter writer = new StreamWriter(FilePath)) {
        var jSerializer = new JsonSerializer();
        jSerializer.Serialize(writer, golfers);
        writer.Close();
      }
    }
  }
}
