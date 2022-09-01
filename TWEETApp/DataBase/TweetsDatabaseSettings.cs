namespace TWEETApp.Databasesettings
{
    public class TweetsDatabaseSettings : ITweetsDatabasesettings
    {
        public string TweetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
