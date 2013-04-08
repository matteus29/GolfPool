using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Golf.DougieApi {
  public class DougieClient : HttpClient {
    private const string BaseAddressUri = "http://cryptic-garden-8260.herokuapp.com/golf/";
    
    public string Endpoint { get; set; }    

    public DougieClient(string uri) {
      this.BaseAddress = new Uri(BaseAddressUri);
      Endpoint = uri;
    }

    public dynamic Git() {
      return base.GetAsync(Endpoint).Result.Content.ReadAsStringAsync().Result;
    }
    public HttpResponseMessage Git(string endpoint) {
      return base.GetAsync(endpoint).Result;
    }
  }
}
