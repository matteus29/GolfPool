//using System;
//using GolfGallery;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
//using System.Text;

//namespace GolfDraftApp.Tests {
//  public class TestResources {

//    public static string BaseFilePath = ASCIIEncoding.ASCII.GetString(
//                        Convert.FromBase64String(
//                        File.ReadAllLines(
//                        Path.Combine(
//                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\\host.db")
//                        )[1])) + @"\GolfDraftApp\Docs\Test\";

//    public List<Golfer> Golfers {get;set;}
//    public List<User> Users {get;set;}
//    public User UserWithFourGolfers {get;set;}
//    public Draft DraftFakeInfo  {get;set;}
//    public Draft Draft1 {get;set;}

//    public TestResources() {
//      Golfers = new List<Golfer> {
//              new Golfer {
//                Id = 1229,
//                FirstName = "Brandt",
//                LastName = "Snedeker",
//                FullName = "Brandt Snedeker",
//                DisplayName = "B. Snedeker",
//                ShortName = "BS",
//                Link = "https://www.google.com/?q=brandt%20snedeker"
//              },
//              new Golfer {
//                Id = 7777,
//                FirstName = "Rory",
//                LastName = "McIlroy",
//                FullName = "Rory McIlroy",
//                DisplayName = "R. McIlroy",
//                ShortName = "RM",
//                Link = "https://www.google.com/?q=rory%20mcilroy"
//              },
//              new Golfer {
//                Id = 1337,
//                FirstName = "Phil",
//                LastName = "Mickelson",
//                FullName = "Phil Mickelson",
//                DisplayName = "P. Mickelson",
//                ShortName = "PM",
//                Link = "https://www.google.com/?q=phil%mickelson"
//              },
//              new Golfer {
//                Id = 9975,
//                FirstName = "Tiger",
//                LastName = "Woods",
//                FullName = "Tiger Woods",
//                DisplayName = "T. Woods",
//                ShortName = "T1G",
//                Link = "https://www.google.com/?q=bigotry"
//              }
//            }; 

//      Users = new List<User> {
//              new User(1, "Matt Simon", Golfers.Select(i => i.Id).ToList().GetRange(0,2)),
//              new User(2, "Kevin Phillips", Golfers.Select(i => i.Id).ToList().GetRange(2,2))
//      };

//      UserWithFourGolfers = new User(1, "Test User", Golfers.Select(i => i.Id).ToList());

//      //DraftFakeInfo = new Draft {
        
//      //  TournamentId = 1,
//      //  Picks = new List<Pick> {
//      //            new Pick(1,1111),
//      //            new Pick(2,2222),
//      //            new Pick(3,3333),
//      //            new Pick(1,4444),
//      //            new Pick(2,5555),
//      //            new Pick(3,6666)
//      //          }
//      //};

//      //Draft1 = new Draft {
//      //  TournamentName = "MRI Duffer's Classic",
//      //  Picks = new List<Pick> {
//      //            new Pick(1,1229),
//      //            new Pick(2,1337),
//      //            new Pick(1,7777),
//      //            new Pick(2,9975)
//      //          }
//      //};
//    }
//  }
//}
