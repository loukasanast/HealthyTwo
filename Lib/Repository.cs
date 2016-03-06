using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Nito.AsyncEx;
using System.Net;
using Windows;

namespace Lib
{
    public class Repository
    {
        private static string token;
        private static WebClient client;
        private static Profile profile;

        public static async Task<string> Test()
        {
            return await Util.GetAccessToken();
        }

        public static async Task<Profile> GetProfileAsync()
        {
            token = await Util.GetAccessToken();
            client = new WebClient();
            string json;

            client.Headers["Authorization"] = string.Format("bearer {0}", token);

            json = await Task.Run<string>(() => client.DownloadString("https://api.microsofthealth.net/v1/me/Profile/"));

            profile = JsonConvert.DeserializeObject<Profile>(json);

            return profile;
        }
    }
}
