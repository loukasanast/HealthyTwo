using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Repository
    {
        public static WebClient Client { get; set; }

        public static void Test()
        {
            Client = new WebClient();
            //Client.Headers.Add("Authorization: bearer GQVfuojjAOHWk9ESwwUnlTMs7CTM4ZKF");
            //Client.Headers["Authorization"] = "bearer GQVfuojjAOHWk9ESwwUnlTMs7CTM4ZKF";
            String result = Client.DownloadString("http://bing.com");
            //Console.WriteLine(result);
            //Console.Read();
            Debug.WriteLine(result); 
        }
    }
}
