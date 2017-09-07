using System.Data.Entity;

namespace OpenWeather.Models.ForDb
{
      public class ConnectToDb : DbContext 
    {
        
        public ConnectToDb()  :base("CitiesDb")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ConnectToDb>());
        }
        public DbSet<CityForSearch> Cities { get; set; }
        public DbSet<coord> Coord { get; set; }
    }
}