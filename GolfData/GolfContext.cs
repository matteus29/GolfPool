using System.Data.Entity;
using Golf.Gallery;

namespace Golf.Data {
  public class GolfContext : DbContext {
    public DbSet<Golfer> Golfers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Pick> Picks { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Score> Scores { get; set; }
  }
}