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

namespace Lib
{
    public class Repository
    {
        private static string token;
        private static WebClient client;
        private static Profile profile;
        private static ActivitiesList activities;

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
            JObject jsonActivities;
            client = new WebClient();
            string json;
            IActivity bike = new BikeActivity();
            IActivity freePlay = new FreePlayActivity();
            IActivity golf = new GolfActivity();
            IActivity guidedWorkout = new GuidedWorkoutActivity();
            IActivity run = new RunActivity();
            IActivity sleep = new SleepActivity();
            activities = new ActivitiesList(6) { bike, freePlay, golf, guidedWorkout, run, sleep };

            return Task.Run<ActivitiesList>(async () => {
                Util.StartServer();

                json = await Task.Run<string>(() => client.DownloadString("http://localhost:8080/"));
                jsonActivities = JObject.Parse(json);

                bike.Period = (TimeSpan)jsonActivities["bikeActivities"].Children()["activitySegments"].Children()["distanceSummary"]["period"].ToList().SingleOrDefault();
                bike.TotalDistance = (int)jsonActivities["bikeActivities"].Children()["activitySegments"].Children()["distanceSummary"]["totalDistance"].ToList().SingleOrDefault();
                bike.Speed = (int)jsonActivities["bikeActivities"].Children()["activitySegments"].Children()["distanceSummary"]["speed"].ToList().SingleOrDefault();
                bike.AverageHeartRate = (int)jsonActivities["bikeActivities"].Children()["activitySegments"].Children()["heartRateSummary"]["averageHeartRate"].ToList().SingleOrDefault();

                freePlay.Period = (TimeSpan)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["distanceSummary"]["period"].ToList().SingleOrDefault();
                freePlay.TotalDistance = (int)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["distanceSummary"]["totalDistance"].ToList().SingleOrDefault();
                freePlay.Speed = (int)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["distanceSummary"]["speed"].ToList().SingleOrDefault();
                freePlay.AverageHeartRate = (int)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["heartRateSummary"]["averageHeartRate"].ToList().SingleOrDefault();

                golf.Period = (TimeSpan)jsonActivities["golfActivities"].Children()["activitySegments"].Children()["period"].ToList().SingleOrDefault();
                golf.TotalDistance = (int)jsonActivities["golfActivities"].Children()["activitySegments"].Children()["distanceWalked"].ToList().SingleOrDefault();
                golf.HoleNumber = (int)jsonActivities["golfActivities"].Children()["activitySegments"].Children()["holeNumber"].ToList().SingleOrDefault();
                golf.AverageHeartRate = (int)jsonActivities["golfActivities"].Children()["activitySegments"].Children()["heartRateSummary"]["averageHeartRate"].ToList().SingleOrDefault();

                guidedWorkout.Period = (TimeSpan)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["distanceSummary"]["period"].ToList().SingleOrDefault();
                guidedWorkout.TotalDistance = (int)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["distanceSummary"]["totalDistance"].ToList().SingleOrDefault();
                guidedWorkout.Speed = (int)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["distanceSummary"]["speed"].ToList().SingleOrDefault();
                guidedWorkout.AverageHeartRate = (int)jsonActivities["freePlayActivities"].Children()["activitySegments"].Children()["heartRateSummary"]["averageHeartRate"].ToList().SingleOrDefault();

                run.Period = (TimeSpan)jsonActivities["runActivities"].Children()["activitySegments"].Children()["distanceSummary"]["period"].ToList().SingleOrDefault();
                run.TotalDistance = (int)jsonActivities["runActivities"].Children()["activitySegments"].Children()["distanceSummary"]["totalDistance"].ToList().SingleOrDefault();
                run.Speed = (int)jsonActivities["runActivities"].Children()["activitySegments"].Children()["distanceSummary"]["speed"].ToList().SingleOrDefault();
                run.AverageHeartRate = (int)jsonActivities["runActivities"].Children()["activitySegments"].Children()["heartRateSummary"]["averageHeartRate"].ToList().SingleOrDefault();

                sleep.Period = (TimeSpan)jsonActivities["sleepActivities"].Children()["activitySegments"].Children()["period"].ToList().SingleOrDefault();
                sleep.StartTime = (DateTime)jsonActivities["sleepActivities"].Children()["activitySegments"].Children()["startTime"].ToList().SingleOrDefault();
                sleep.Duration = (int)jsonActivities["sleepActivities"].Children()["activitySegments"].Children()["duration"].ToList().SingleOrDefault();
                sleep.AverageHeartRate = (int)jsonActivities["sleepActivities"].Children()["activitySegments"].Children()["heartRateSummary"]["averageHeartRate"].ToList().SingleOrDefault();

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
    }
}
