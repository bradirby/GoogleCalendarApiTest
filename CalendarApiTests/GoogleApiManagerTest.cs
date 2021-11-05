using System;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using GoogleApiTest;
using NUnit.Framework;

namespace CalendarApiTests
{
   
    public class GoogleApiManagerTest
    {

        [Test]
        [Explicit]
        public void CreateCalSvcWithApiKey()
        {
            try
            {

                var apiKey = Environment.GetEnvironmentVariable("GoogleCalendarApiTest_ApiKey");
                Assert.IsFalse(string.IsNullOrEmpty(apiKey), "Could not find apiKey");

                string appName = "My Test Google Calendar API App";
                var calMgr = GoogleApiManagerFactory.GetManagerUsingApiKey(appName, apiKey);

                string[] scopes = { CalendarService.Scope.CalendarReadonly };
                var svc = calMgr.GetCalendarService(scopes);

                var calendarId = Environment.GetEnvironmentVariable("GoogleCalendarApiTest_CalendarId");
                Assert.IsFalse(string.IsNullOrEmpty(calendarId), "Could not find calendarId");

                var getRequest = svc.CalendarList.Get(calendarId);
                var result = getRequest.ExecuteAsync().Result;
                Assert.IsNotNull(result);
                Assert.AreEqual("asdf", result.Description);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

     
        [Test]
        [Explicit]
        public void GetDiscoveryService()
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("GoogleCalendarApiTest_ApiKey");
                Assert.IsFalse(string.IsNullOrEmpty(apiKey), "Could not find apiKey");

                string appName = "My Test Google Calendar API App";
                var calMgr = GoogleApiManagerFactory.GetManagerUsingApiKey(appName, apiKey);

                var discoverySvc = calMgr.GetDiscoveryService();

                var result = discoverySvc.Apis.List().ExecuteAsync().Result;

                Assert.IsNotNull(result);
                Assert.Greater(0, result.Items.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
