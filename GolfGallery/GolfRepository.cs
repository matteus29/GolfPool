using System.Collections.Generic;
using System.Linq;
using Golf.DougieApi;

namespace Golf.Gallery {
  public class GolfRepository {
    public const string BASEFILEPATH = @"C:\Users\mattv_000\Dropbox\GolfDraftApp\Docs\";

    public List<User> Users { get; set; }
    public List<Golfer> Golfers { get; set; }
    public Tournament Tournament { get; set; }
    public List<Pick> Picks { get; set; }

    private static List<Golfer> _allGolfers;
    public static List<Golfer> AllGolfers {
      get {
        if (_allGolfers != null)
          return _allGolfers;

        _allGolfers = GetAllGolfers();
        return _allGolfers;
      }
    }

    public GolfRepository(bool defaultConfig = true) {
      if (defaultConfig) Init();
    }

    private void Init() {
      Users = new List<User>();
      Golfers = new List<Golfer>();
      Tournament = new Tournament();

      var uReader = new UserReader(BASEFILEPATH + @"\users.golfuser");
      Users = uReader.ReadAllUsersFromFile();
    }

    public void Add(User user) {
      if (Users == null)
        Users = new List<User>();
      Users.Add(user);
    }
    public void Add(List<User> users) {
      if (Users == null)
        Users = new List<User>();
      Users.AddRange(users);
    }

    public void Add(Golfer golfer) {
      if (Golfers == null)
        Golfers = new List<Golfer>();
      Golfers.Add(golfer);
    }
    public void Add(List<Golfer> golfers) {
      if (Golfers == null)
        Golfers = new List<Golfer>();
      Golfers.AddRange(golfers);
    }

    //public int GetUserScore(int userId, int round) {
    //  return Golfers.Where(g => Users.First(u => u.Id == userId).Golfers.Contains(g.Id))
    //    .Select(s => s.Scores)
    //    .Select(r => r[round-1]).ToList()
    //    .OrderBy(j => j).Take(2).Sum();
    //}
    //public int GetUserScore(int userId) {
    //  var sum = 0;
    //  for (int round = 1; round <= 4; round++) {
    //    sum += GetUserScore(userId, round);
    //  }
    //  return sum;
    //}

    private static List<Golfer> GetAllGolfers() {
      return new GolferReader().ReadAllGolfersFromFile(BASEFILEPATH + @"GolferList.golfer");
    }
  }
}
