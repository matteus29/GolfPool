using Newtonsoft.Json;

namespace Golf.Gallery {
  public class Score {
    [JsonProperty("IGNORE")]
    public int Id { get; set; }
    [JsonProperty("id")]
    public int GolferEspnId { get; set; }
    public int TournamentId { get; set; }
    public int ToPar { get; set; }
    [JsonProperty("scores")]
    public int[] Scores { get; set; }
    public string RoundProgress { get; set; }
    public int RoundToPar { get; set; }
  }
}
