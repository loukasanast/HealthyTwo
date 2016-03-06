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
        private static AuthData account = new AuthData();
        private static readonly AsyncLock _lock = new AsyncLock();
        
        internal static async Task<string> GetRefreshToken()
        {
            WebClient client = new WebClient();
            string token;
            string temp;

            using(var releaser = await _lock.LockAsync())
            {
                token = ReadRefreshToken().Result;
            }

            using(Stream stream = client.OpenRead(string.Format("https://login.live.com/oauth20_token.srf?client_id=000000004418090B&redirect_uri=http://anastasiou.engineer&client_secret=GQVfuojjAOHWk9ESwwUnlTMs7CTM4ZKF&refresh_token={0}&grant_type=refresh_token", token)))
            using(StreamReader sr = new StreamReader(stream))
            {
                temp  = await sr.ReadToEndAsync();
            }

            account = JsonConvert.DeserializeObject<AuthData>(temp);
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
    }
}
