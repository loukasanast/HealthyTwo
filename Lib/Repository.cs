using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using System.Net;
using Windows;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;

namespace Lib
{
    public class Repository
    {
        private static string token;
        private static WebClient client;
        private static Profile profile;
        private static ActivitiesList activities;
        private static Summaries summaries;

        public static async Task<string> Test()
        {
            return await Util.GetAccessToken();
        }

        public static Task<Profile> GetProfileAsync()
        {
            return Task.Run<Profile>(async () =>
            {
                token = await Util.GetAccessToken();
                client = new WebClient();
                string json;

                client.Headers["Authorization"] = string.Format("bearer {0}", token);

                json = await Task.Run<string>(() => client.DownloadString("https://api.microsofthealth.net/v1/me/Profile/"));

                profile = JsonConvert.DeserializeObject<Profile>(json);

                return profile;
            });
        }

        public static Task<ActivitiesList> GetActivitiesAsync()
        {         
            return Task.Run<ActivitiesList>(async () => 
            {
                JObject jsonActivities;
                client = new WebClient();
                string json;
                activities = new ActivitiesList(6);

                Util.StartServer();

                json = await Task.Run<string>(() => client.DownloadString("http://localhost:8080/"));
                jsonActivities = JObject.Parse(json);

                activities = await Util.GetActivitiesAsync(json);

                return activities;
            });
        }

        public static Task<DevicesList> GetDevicesAsync()
        {
            return Task.Run<DevicesList>(async () =>
            {
                DevicesList result = new DevicesList(3);
                string connString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

                using(SqlConnection conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM DEVICES";

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if(reader.HasRows)
                    {
                        while(await reader.ReadAsync())
                        {
                            Device temp = new Device();
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
            });
        }

        public static Task AddDeviceAsync(Device dev)
        {
            return Task.Run(async () =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

                using(SqlConnection conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Devices VALUES(@displayName, @lastSuccessfulSync, @deviceFamily, @hardwareVersion, @softwareVersion, @modelName, @manufacturer, @deviceStatus, @createdDate)";
                    SqlParameter parameter1 = new SqlParameter("displayName", SqlDbType.VarChar, 50, "DisplayName") { Value = dev.DisplayName };
                    cmd.Parameters.Add(parameter1);
                    SqlParameter parameter2 = new SqlParameter("lastSuccessfulSync", SqlDbType.DateTime, 0, "LastSuccessfulSync") { Value = dev.LastSuccessfulSync };
                    cmd.Parameters.Add(parameter2);
                    SqlParameter parameter3 = new SqlParameter("deviceFamily", SqlDbType.VarChar, 50, "DeviceFamily") { Value = dev.DeviceFamily };
                    cmd.Parameters.Add(parameter3);
                    SqlParameter parameter4 = new SqlParameter("hardwareVersion", SqlDbType.VarChar, 50, "HardwareVersion") { Value = dev.HardwareVersion };
                    cmd.Parameters.Add(parameter4);
                    SqlParameter parameter5 = new SqlParameter("softwareVersion", SqlDbType.VarChar, 50, "SoftwareVersion") { Value = dev.SoftwareVersion };
                    cmd.Parameters.Add(parameter5);
                    SqlParameter parameter6 = new SqlParameter("modelName", SqlDbType.VarChar, 50, "ModelName") { Value = dev.ModelName };
                    cmd.Parameters.Add(parameter6);
                    SqlParameter parameter7 = new SqlParameter("manufacturer", SqlDbType.VarChar, 50, "Manufacturer") { Value = dev.Manufacturer };
                    cmd.Parameters.Add(parameter7);
                    SqlParameter parameter8 = new SqlParameter("deviceStatus", SqlDbType.VarChar, 50, "DeviceStatus") { Value = dev.DeviceStatus };
                    cmd.Parameters.Add(parameter8);
                    SqlParameter parameter9 = new SqlParameter("createdDate", SqlDbType.DateTime, 0, "CreatedDate") { Value = dev.CreatedDate };
                    cmd.Parameters.Add(parameter9);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public static Task<Summaries> GetSummariesAsync()
        {
            return Task.Run<Summaries>(async () =>
            {
                JObject jsonActivities;
                ActivitiesList temp;
                PropertyInfo[] pInfoArr = typeof(IActivity).GetProperties();
                int distanceSum = 0;
                int speedSum = 0;
                int heartRateSum = 0;
                client = new WebClient();
                string json;
                summaries = new Summaries();

                Util.StartServer();

                json = await Task.Run<string>(() => client.DownloadString("http://localhost:8080/"));
                jsonActivities = JObject.Parse(json);

                temp = await Util.GetActivitiesAsync(json);
                summaries.Period = temp[0].Period;

                foreach(IActivity a in temp)
                {
                    if(a is GolfActivity || a is SleepActivity) continue;

                    distanceSum += a.TotalDistance;
                    speedSum += a.Speed;
                    heartRateSum += a.AverageHeartRate;
                }

                summaries.TotalDistance = distanceSum;
                summaries.Speed = speedSum / 4;
                summaries.AverageHeartRate = heartRateSum / 4;

                return summaries;
            });
        }
    }
}
