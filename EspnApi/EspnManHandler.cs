using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using GolfGallery;

namespace EspnApi {
  public class EspnManHandler {

    private const string _key = "apikey=p8dcjznss6ukkmrc3rcptbrk";

    public List<Golfer> GetGolfersFromEspn(int offset = 0, int golferCount = 1000) {
      return GetAllGolfersFromEspn(offset, golferCount).Take(golferCount).ToList();
    }

    private List<Golfer> GetAllGolfersFromEspn(int offset, int golferCount) {
      var golfers = new List<Golfer>();

      var client = new HttpClient();
      client.BaseAddress = new Uri("http://api.espn.com/v1/sports/");
      
      var response = client.GetAsync(string.Format("golf/athletes?offset={0}&{1}", offset, _key)).Result;
      int resultsCount = int.MaxValue;

      while (response.IsSuccessStatusCode && golfers.Count < golferCount && offset < resultsCount) {
        var rawJson = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(rawJson);
        var jAthletes = jObject.SelectToken("sports[0].leagues[0].athletes").ToList();

        resultsCount = (int)jObject.SelectToken("resultsCount");

        jAthletes.ForEach(j => {
          var currentGolfer = j.ToObject<Golfer>();
          if (!golfers.Any(b => b.FullName == currentGolfer.FullName)) {
            golfers.Add(j.ToObject<Golfer>());
            golfers.Last().Link = j["links"]["web"]["athletes"]["href"].ToObject<string>();
          }
        });
        offset += 50;
        Thread.Sleep(1000);
        response = client.GetAsync(string.Format("golf/athletes?offset={0}&{1}", offset, _key)).Result;
        Thread.Sleep(1000);
      }
      return golfers;
    }
  }
}
