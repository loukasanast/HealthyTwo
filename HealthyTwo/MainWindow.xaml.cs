using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Threading;
using Lib;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace HealthyTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel vm { get; set; }

        public MainWindow()
        {
            vm = new ViewModel();
            vm.Devices = new DevicesList(3);
            vm.Profile = new Profile();

            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void imgMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void imgInstagram_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.instagram.com/loukasanast/");
        }

        private void imgFacebook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.facebook.com/loukas.anastasiou.311");
        }

        private void lblAct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdActivities.Opacity == 0)
            {
                PageFadeIn(grdActivities);
            }
        }

        private async void lblDev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdDevices.Opacity == 0)
            {
                //string connString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

                //using(SqlConnection conn = new SqlConnection(connString))
                //{
                //    conn.Open();

                //    SqlCommand cmd = conn.CreateCommand();
                //    cmd.CommandType = CommandType.Text;
                //    cmd.CommandText = "SELECT * FROM DEVICES";

                //    SqlDataReader reader = cmd.ExecuteReader();

                //    if(reader.HasRows)
                //    {
                //        Device temp = new Device();
                //        vm.Devices.Clear();

                //        while(reader.Read())
                //        {
                //            temp.Id = reader.GetInt32(0);
                //            temp.DisplayName = reader.GetString(1);
                //            temp.LastSuccessfulSync = reader.GetDateTime(2);

                //            DeviceFamily tempDevFamily = new DeviceFamily();
                //            temp.DeviceFamily = (Enum.TryParse<DeviceFamily>(reader.GetString(3), out tempDevFamily) ? tempDevFamily : DeviceFamily.Mobile);

                //            temp.HardwareVersion = reader.GetString(4);
                //            temp.SoftwareVersion = reader.GetString(5);
                //            temp.ModelName = reader.GetString(6);
                //            temp.Manufacturer = reader.GetString(7);

                //            DeviceStatus tempDevStatus = new DeviceStatus();
                //            temp.DeviceStatus = (Enum.TryParse<DeviceStatus>(reader.GetString(8), out tempDevStatus) ? tempDevStatus : DeviceStatus.Off);

                //            temp.CreatedDate = reader.GetDateTime(9);

                //            vm.Devices.Add(temp);
                //        }
                //    }
                //}


                vm.Devices = await Repository.GetDevicesAsync();

                Label lblDevDisplayName = null;
                string[] titles = { "Id", "", "Sync", "Family", "Hardware", "Software", "Model", "Manufact.", "Status", "Created" };
                object[] toAdd = new object[2];
                PropertyInfo[] pInfoArr = typeof(Device).GetProperties();

                for(int i = 0; i < vm.Devices.Count; i++)
                {
                    for(int j = 0; j < grdDevices.Children.OfType<Grid>().Count(); j++)
                    {
                        grdDevices.Children.Remove(grdDevices.Children.OfType<Grid>().ElementAt<Grid>(j));
                    }

                    Grid grdDev = new Grid();
                    grdDev.Opacity = (i % 2 == 0 ? 1 : .4);
                    grdDevices.Children.Add(grdDev);

                    if(lblDevDisplayName == null)
                    {
                        lblDevDisplayName = new Label();
                        lblDevDisplayName.Content = vm.Devices[i].DisplayName;
                        lblDevDisplayName.HorizontalAlignment = HorizontalAlignment.Left;
                        lblDevDisplayName.Margin = new Thickness(283 + i * 128, 97, 0, 0);
                        lblDevDisplayName.VerticalAlignment = VerticalAlignment.Top;
                        lblDevDisplayName.Foreground = new SolidColorBrush(new Color() { R = 255, G = 25, B = 43, A = 255 });
                        lblDevDisplayName.FontFamily = new FontFamily("Aller Display");
                        lblDevDisplayName.FontSize = 11;
                        grdDev.Children.Add(lblDevDisplayName);
                    }

                    for(int j = 0; j < typeof(Device).GetProperties().Length; j++)
                    {
                        if(j == 1) continue;
                        string propName = pInfoArr[j].PropertyType == typeof(DateTime) ? ((DateTime)pInfoArr[j].GetValue(vm.Devices[i])).ToString("d") : pInfoArr[j].GetValue(vm.Devices[i]).ToString();
                        toAdd = PrepDevControls(titles[j], propName, i, j > 1 ? j - 1 : j);
                        grdDev.Children.Add((Label)toAdd[0]);
                        grdDev.Children.Add((TextBlock)toAdd[1]);
                    }

                    AddImg(grdDev, "top-corner.png", 8, 8, new Thickness(264 + (i * 168), 80, 528 - (i * 168), 378));
                    AddImg(grdDev, "bottom-corner.png", 8, 8, new Thickness(425 + (i * 168), 417, 367 - (i * 168), 41));
                }

                PageFadeIn(grdDevices);
            }

        }

        private async void lblPro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdProfile.Opacity == 0)
            {
                vm.Profile = await Repository.GetProfileAsync();
                vm.OnPropertyChanged("Profile");
                vm.Profile.OnPropertyChanged("FirstName");
                PageFadeIn(grdProfile);
            }
        }

        private void lblSum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdSummaries.Opacity == 0)
            {
                PageFadeIn(grdSummaries);
            }
        }

        private void imgCloseAct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PageFadeIn(grdHome);
        }

        private void imgCloseDev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PageFadeIn(grdHome);
        }

        private void imgCloseProf_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PageFadeIn(grdHome);
        }

        private void imgCloseSum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PageFadeIn(grdHome);
        }

        private void imgHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdHome.Opacity == 0)
            {
                PageFadeIn(grdHome);
            }
        }

        private void imgAddDev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(vm.Devices.Count < 3)
            {
                Grid addGrid = new Grid();
                string[] titles = { "Name", "Sync", "Family", "Hardware", "Software", "Model", "Manufact.", "Status", "Created" };
                object[] toAdd = new object[2];

                for(int i = 0; i < titles.Length; i++)
                {
                    toAdd = PrepAddControls(titles[i], vm.Devices.Count, i);
                    addGrid.Children.Add((Label)toAdd[0]);
                    addGrid.Children.Add((TextBox)toAdd[1]);
                }

                AddImg(addGrid, "top-corner.png", 8, 8, new Thickness(264 + vm.Devices.Count * 168, 80, 528 - vm.Devices.Count * 168, 378));
                AddImg(addGrid, "bottom-corner.png", 8, 8, new Thickness(425 + vm.Devices.Count * 168, 417, 367 - vm.Devices.Count * 168, 41));
                AddImg(addGrid, "upload-hover.png", 16, 16, new Thickness(288 + vm.Devices.Count * 168, 96, 496 - vm.Devices.Count * 168, 354));
                AddImg(addGrid, "upload.png", 16, 16, new Thickness(288 + vm.Devices.Count * 168, 96, 496 - vm.Devices.Count * 168, 354), (Style)TryFindResource("ImgFadeOut"));
                AddImg(addGrid, "discart-hover.png", 16, 16, new Thickness(312 + vm.Devices.Count * 168, 96, 468 - vm.Devices.Count * 168, 354));
                AddImg(addGrid, "discart.png", 16, 16, new Thickness(312 + vm.Devices.Count * 168, 96, 468 - vm.Devices.Count * 168, 354), (Style)TryFindResource("ImgFadeOut")).MouseDown += (s, eArgs) =>
                {
                    grdDevices.Children.Remove(addGrid);
                };

                grdDevices.Children.Add(addGrid);
            }
            else
            {
                MessageBox.Show("You are not allowed to add more than 3 devices", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void imgHyperlinkPro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://dashboard.microsofthealth.com/#/profile/");
        }

        private void PageFadeIn(Grid g)
        {
            if(grdActivities.Opacity != 0)
                ((Storyboard)FindResource("PageFadeOut")).Begin(grdActivities);
            else if(grdDevices.Opacity != 0)
                ((Storyboard)FindResource("PageFadeOut")).Begin(grdDevices);
            else if(grdProfile.Opacity != 0)
                ((Storyboard)FindResource("PageFadeOut")).Begin(grdProfile);
            else if(grdSummaries.Opacity != 0)
                ((Storyboard)FindResource("PageFadeOut")).Begin(grdSummaries);
            else
                ((Storyboard)FindResource("PageFadeOut")).Begin(grdHome);

            g.Visibility = Visibility.Visible;
            DoubleAnimation anim = new DoubleAnimation(1, TimeSpan.FromSeconds(.14));
            g.BeginAnimation(Grid.OpacityProperty, anim);
        }

        private object[] PrepDevControls(string key, string value, int col, int row)
        {
            object[] result = new object[2];
            
            Label lbl = new Label();
            lbl.Content = key;
            lbl.HorizontalAlignment = HorizontalAlignment.Left;
            lbl.Margin = new Thickness(283 + col * 128, 123 + row * 32, 0, 0);
            lbl.VerticalAlignment = VerticalAlignment.Top;
            lbl.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
            lbl.FontFamily = new FontFamily("Nobile");
            lbl.FontSize = 10;
            result[0] = lbl;

            TextBlock tbk = new TextBlock();
            tbk.Text = value;
            tbk.HorizontalAlignment = HorizontalAlignment.Left;
            tbk.Margin = new Thickness(311 + col * 128, 128 + row * 32, 0, 0);
            tbk.VerticalAlignment = VerticalAlignment.Top;
            tbk.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
            tbk.FontFamily = new FontFamily("Nobile");
            tbk.FontSize = 10;
            tbk.Width = 100;
            tbk.TextAlignment = TextAlignment.Right;
            result[1] = tbk;

            return result;
        }

        private object[] PrepAddControls(string key, int col, int row)
        {
            object[] result = new object[2];

            Label lbl = new Label();
            lbl.Content = key;
            lbl.HorizontalAlignment = HorizontalAlignment.Left;
            lbl.Margin = new Thickness(283 + col * 168, 123 + row * 32, 0, 0);
            lbl.VerticalAlignment = VerticalAlignment.Top;
            lbl.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
            lbl.FontFamily = new FontFamily("Nobile");
            lbl.FontSize = 10;
            result[0] = lbl;

            TextBox txt = new TextBox();
            txt.HorizontalAlignment = HorizontalAlignment.Left;
            txt.Margin = new Thickness(341 + col * 168, 124 + row * 32, 0, 0);
            txt.VerticalAlignment = VerticalAlignment.Top;
            txt.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
            txt.Background = new SolidColorBrush(new Color() { R = 0, G = 0, B = 0, A = 255 });
            txt.FontFamily = new FontFamily("Nobile");
            txt.FontSize = 10;
            txt.Width = 70;
            txt.TextAlignment = TextAlignment.Right;
            result[1] = txt;

            return result;
        }

        private Image AddImg(Grid c, string f, int w, int h, Thickness t, Style s = null)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,,/HealthyTwo;component/img/" + f));
            img.Width = w;
            img.Height = h;
            img.Margin = t;
            img.Style = s;
            c.Children.Add(img);

            return img;
        }
    }
}
