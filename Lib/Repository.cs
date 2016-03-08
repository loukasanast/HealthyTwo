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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

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

        public static async Task<DevicesList> GetDevicesAsync()
        {
            DevicesList result = new DevicesList(3);
            string connString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

            using(SqlConnection conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM DEVICES";

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    Device temp = new Device();

                    while(await reader.ReadAsync())
                    {
                        temp.Id = reader.GetInt32(0);
                        temp.DisplayName = reader.GetString(1);
                        temp.LastSuccessfulSync = reader.GetDateTime(2);

                        DeviceFamily tempDevFamily = new DeviceFamily();
                        temp.DeviceFamily = (Enum.TryParse<DeviceFamily>(reader.GetString(3), out tempDevFamily) ? tempDevFamily : DeviceFamily.Mobile);

                        temp.HardwareVersion = reader.GetString(4);
                        temp.SoftwareVersion = reader.GetString(5);
                        temp.ModelName = reader.GetString(6);
                        temp.Manufacturer = reader.GetString(7);

                        DeviceStatus tempDevStatus = new DeviceStatus();
                        temp.DeviceStatus = (Enum.TryParse<DeviceStatus>(reader.GetString(8), out tempDevStatus) ? tempDevStatus : DeviceStatus.Off);

                        temp.CreatedDate = reader.GetDateTime(9);

                        result.Add(temp);
                    }
                }
            }

            return result;
        }
    }
}
