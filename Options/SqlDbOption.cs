namespace MDDPlatform.DataStorage.SQLDB.Options
{
    public class SqlDbOption
    {
        public string ConnectionString { get; set; }
        public bool Seed {get;set;}
        public bool Migrate {get;set;}
    }
}