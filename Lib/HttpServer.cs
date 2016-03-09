using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib
{
    class HttpServer
    {
        internal HttpListener Listener { get; set; }
        internal HttpListenerContext Context { get; set; }
        internal HttpListenerResponse Response { get; set; }
        public bool IsInit { get; set; }
        private static HttpServer singleton;

        private HttpServer()
        {
            Listener = new HttpListener();
        }

        public static HttpServer GetHttpServer()
        {
            if(singleton == null)
            {
                singleton = new HttpServer();
            }

            return singleton;
        }

        public bool TryStart()
        {
            if(!IsInit)
            {
                try
                {
                    Listener.Prefixes.Add("http://localhost:8080/");
                    Listener.Start();
                    IsInit = true;
                    return true;
                }
                catch
                {
                    ConsoleColor temp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    MessageBox.Show("Could not initiate the server.");
                    Console.ForegroundColor = temp;
                    Console.Read();
                    return false;
                }
            }

            return true;
        }

        public async void Respond()
        {
            byte[] data;

            try
            {
                using(FileStream fs = new FileStream("activities.json", FileMode.Open, FileAccess.Read))
                {
                    data = new byte[fs.Length];
                    await fs.ReadAsync(data, 0, data.Length);
                }

                using(Stream output = Response.OutputStream)
                {
                    output.Write(data, 0, data.Length);
                }
            }
            catch(Exception exc)
            {
                ConsoleColor temp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("Could not responde to the request.\n" + exc.Message);
                Console.ForegroundColor = temp;
                Console.Read();
            }
        }
    }
}
