using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OpenWeather.Models.ForDb
{
      public class ConnectToDb : DbContext 
    {
        
        public ConnectToDb()  :base("Cities")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ConnectToDb>());
        }
        public DbSet<CityForSearch> Cities { get; set; }
        coord
    }
}