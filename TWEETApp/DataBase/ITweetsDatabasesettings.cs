namespace TWEETApp.Databasesettings
{
    public interface ITweetsDatabasesettings
    {
        public string TweetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}