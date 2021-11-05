using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Discovery.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Calendar.v3.CalendarService;

namespace GoogleApiTest
{
    public class GoogleApiManager
    {
        private BaseClientService.Initializer initializer;

        internal GoogleApiManager( )
        {
        }


        internal GoogleApiManager InitForApiKey(string myApp, string apikey)
        {
            try
            {
                initializer = new BaseClientService.Initializer()
                {
                    ApiKey = apikey, 
                    ApplicationName = myApp
                };

                return this;
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw  ;
            }


        }

        internal async Task<GoogleApiManager> InitForOauthServiceAsync(string myApp, string usr, string clientSecrets, string[] scopes)
        {
            try
            {
                UserCredential credential;

                using (var credentialFileStream = new FileStream(clientSecrets, FileMode.Open, FileAccess.Read))
                {
                    var secretsFile = await GoogleClientSecrets.FromStreamAsync(credentialFileStream);
                
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    var fileDataStore = new FileDataStore(credPath, true);
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        secretsFile.Secrets,
                        scopes,
                        usr,
                        CancellationToken.None,
                        fileDataStore);
                }

                initializer = new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = myApp,
                };

                return this;
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw  ;
            }
        }


        public CalendarService GetCalendarService()
        {
            try
            {
                return new CalendarService(initializer);
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw  ;
            }
        }

        public DiscoveryService GetDiscoveryService()
        {
            try
            {
                return new DiscoveryService(initializer);
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw  ;
            }
        }

    }

}
