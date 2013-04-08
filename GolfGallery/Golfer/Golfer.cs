using System.Collections.Generic;

namespace Golf.Gallery {
  public class Golfer {
    public const string FileType = "golfer";

    public int Id { get; set; }
    public int EspnId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Link { get; set; }
  }
}
