using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Golf.DougieApi {
  public class DougieApiHandler {

    public List<int> GetCurrentField(int id) {
      var dClient = new DougieClient("pga/field/" + id);

      dynamic dynamo = JsonConvert.DeserializeObject(dClient.Git());

      var field = new List<int>();

      foreach (var value in dynamo) {
        field.Add((int)value.id.Value);
      }

      return field;
    }

    public dynamic GetLeaderboard(int id) {
      return new DougieClient("pga/leaderboard/" + id).Git();
    }

    public string GetTournamentName() {
      var jObject = JObject.Parse(new DougieClient("pga").Git());
      return jObject.SelectToken("tournament_name").ToString();
    }

    public string GetTournamentPar() {
      var jObject = JObject.Parse(new DougieClient("pga").Git());
      string par = jObject.SelectToken("course").ToString();
      return par.Substring(par.IndexOf("| Par") + 6, 2);
    }

    public int GetTournamentId() {
      var jObject = JObject.Parse(new DougieClient("pga").Git());
      return jObject.SelectToken("tournament_id");
    }
  }

}
