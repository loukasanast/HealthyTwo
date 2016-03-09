using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using Nito.AsyncEx;

namespace Lib
{
    static class Util
    {
        private static WebClient client = new WebClient();
        private static AuthData account = new AuthData();
        private static readonly AsyncLock _lock = new AsyncLock();
        
        internal static async Task<string> GetRefreshToken()
        {
            string token;
            string json;

            using(var releaser = await _lock.LockAsync())
            {
                token = ReadRefreshToken().Result;
            }

            using(Stream stream = client.OpenRead(string.Format("https://login.live.com/oauth20_token.srf?client_id=000000004418090B&redirect_uri=http://anastasiou.engineer&client_secret=GQVfuojjAOHWk9ESwwUnlTMs7CTM4ZKF&refresh_token={0}&grant_type=refresh_token", token)))
            using(StreamReader sr = new StreamReader(stream))
            {
                json  = await sr.ReadToEndAsync();
            }

            account = JsonConvert.DeserializeObject<AuthData>(json);
            token = account.Refresh_token;

            using(var releaser = await _lock.LockAsync())
            {
                StoreRefreshToken(token);
            }

            return token;
        }

        private static Task<string> ReadRefreshToken()
        {
            return Task.Run<string>(() => {
                using(FileStream fs = new FileStream("reft.dat", FileMode.Open, FileAccess.Read))
                using(StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            });
        }

        private static void StoreRefreshToken(string token)
        {
            Task.Run(() => {
                using(FileStream fs = new FileStream("reft.dat", FileMode.Create, FileAccess.Write))
                {
                    fs.Write(Encoding.UTF8.GetBytes(token), 0, Encoding.UTF8.GetByteCount(token));
                }
            });
        }

        internal static async Task<string> GetAccessToken()
        {
            string refToken;
            string json;
            
            refToken = await GetRefreshToken();

            json = await Task.Run<string>(() => {
                return client.DownloadString(string.Format("https://login.live.com/oauth20_token.srf?client_id=000000004418090B&redirect_uri=http://anastasiou.engineer&client_secret=GQVfuojjAOHWk9ESwwUnlTMs7CTM4ZKF&refresh_token={0}&grant_type=refresh_token", refToken));
            });

            account = JsonConvert.DeserializeObject<AuthData>(json);

            return account.Access_token;
        }

        internal static void StartServer()
        {
            Task.Run(() =>
            {
                HttpServer server = HttpServer.GetHttpServer();

                if(server.TryStart())
                {
                    while(true)
                    {
                        server.Context = server.Listener.GetContextAsync().Result;
                        server.Response = server.Context.Response;
                        server.Response.ContentType = "application/json";

                        new Thread(() =>
                        {
                            server.Respond();
                        }).Start();
                    }
                }
            });
        }
    }
}
