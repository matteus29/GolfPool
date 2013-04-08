using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Golf.Gallery;

namespace Golf.Tests.Resources {
  public static class TestData {

    public static User User = new User(1, "User Jones");

    public static Tournament Tourny = new Tournament {
                                                      Id = 1,
                                                      EspnId = 1234,
                                                      Name = "Tournament of Champions",
                                                      Par = 72
                                                    };

    public static List<Golfer> Golfers = new List<Golfer> {
                                                new Golfer {
                                                  Id=1,
                                                  EspnId=99,
                                                  FirstName="Arnold",
                                                  LastName="Palmer",
                                                  FullName="Arnold Palmer"
                                                },
                                                new Golfer {
                                                  Id=2,
                                                  EspnId=88,
                                                  FirstName="Jack",
                                                  LastName="Nicklaus",
                                                  FullName="Jack Nicklaus"
                                                },
                                                new Golfer {
                                                  Id=3,
                                                  EspnId=77,
                                                  FirstName="Gary",
                                                  LastName="Player",
                                                  FullName="Gary Player"
                                                },
                                                new Golfer {
                                                  Id=4,
                                                  EspnId=66,
                                                  FirstName="Lord",
                                                  LastName="Byron",
                                                  FullName="Lord Byron"
                                                }
                                              };

    public static List<Pick> Picks = new List<Pick> {
                                            new Pick {
                                              Id=1,
                                              TournamentId=1,
                                              UserId=1,
                                              GolferId=1
                                            },
                                            new Pick {
                                              Id=2,
                                              TournamentId=1,
                                              UserId=1,
                                              GolferId=2
                                            },
                                            new Pick {
                                              Id=3,
                                              TournamentId=1,
                                              UserId=1,
                                              GolferId=3
                                            },
                                            new Pick {
                                              Id=4,
                                              TournamentId=1,
                                              UserId=1,
                                              GolferId=4
                                            }
    };

  }
}
