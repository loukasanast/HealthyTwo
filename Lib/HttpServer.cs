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

        public HttpServer()
        {
            Listener = new HttpListener();
        }

        public bool TryStart()
        {
            try
            {
                Listener.Prefixes.Add("http://localhost:8080/");
                Listener.Start();
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
