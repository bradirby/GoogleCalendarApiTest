namespace GoogleApiTest
{
    public static class GoogleApiManagerFactory
    {
        public static GoogleApiManager GetManagerUsingApiKey(string myAppName, string myApiKey)
        {
            var mgr = new GoogleApiManager();
            mgr.InitForApiKey(myAppName, myApiKey);
            return mgr;
        }
    }
}