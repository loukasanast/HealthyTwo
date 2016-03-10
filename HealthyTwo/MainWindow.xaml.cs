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
            vm.Activities = new ActivitiesList(6);
            vm.Devices = new DevicesList(3);
            vm.Profile = new Profile();
            vm.Summaries = new Summaries();

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

        private async void lblAct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdActivities.Opacity == 0)
            {
                for(int i = 0; i < grdActivities.Children.OfType<Grid>().Count(); i++)
                {
                    grdActivities.Children.RemoveRange(1, 30);
                    break;
                }

                imgAddDev.MouseDown -= imgAddDev_MouseDown;
                imgAddDev.MouseDown += imgAddDev_MouseDown;

                vm.Activities = await Repository.GetActivitiesAsync();
                PropertyInfo[] pInfoArr = typeof(IActivity).GetProperties();
                object[] toAdd = new object[2];
                Label lblRedTitle;
                string[] titles = { "Bike", "Free Play", "Golf", "Guided Workout", "Run", "Sleep" };

                for(int i = 0; i < vm.Activities.Count; i++)
                {
                    Grid grdAct = new Grid();

                    lblRedTitle = new Label();
                    lblRedTitle.Content = titles[i].ToUpper();
                    lblRedTitle.HorizontalAlignment = HorizontalAlignment.Left;
                    lblRedTitle.Margin = new Thickness(283 + ((i < 3 ? i : i - 3) * 168), 97 + ((i < 3 ? 0 : 1) * 172), 0, 0);
                    lblRedTitle.VerticalAlignment = VerticalAlignment.Top;
                    lblRedTitle.Foreground = new SolidColorBrush(new Color() { R = 255, G = 25, B = 43, A = 255 });
                    lblRedTitle.FontFamily = new FontFamily("Aller Display");
                    lblRedTitle.FontSize = 11;
                    grdActivities.Children.Add(lblRedTitle);

                    AddImg(grdActivities, "top-corner.png", 8, 8, new Thickness(264 + ((i < 3 ? i : i - 3) * 168), 80 + (i > 2 ? 172 : 0), 528 - ((i < 3 ? i : i - 3) * 168), 378 - (i > 2 ? 172 : 0)));
                    AddImg(grdActivities, "bottom-corner.png", 8, 8, new Thickness(425 + ((i < 3 ? i : i - 3) * 168), 245 + (i > 2 ? 172 : 0), 367 - ((i < 3 ? i : i - 3) * 168), 213 - (i > 2 ? 172 : 0)));
                    Image hyperlink;
                    (hyperlink = AddImg(grdActivities, "hyperlink.png", 8, 8, new Thickness(408 + ((i < 3 ? i : i - 3) * 168), 104 + (i > 2 ? 172 : 0), 384 - ((i < 3 ? i : i - 3) * 168), 354 - (i > 2 ? 172 : 0)))).MouseDown += (s, eArgs) => {
                        switch(titles[grdActivities.Children.IndexOf(((Image)s)) / 5])
                        {
                            case "Bike": Process.Start(string.Format("https://dashboard.microsofthealth.com/#/{0}/{1}-{2}-{3}/month/chart", "bike", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                                break;
                            case "Free Play":
                                Process.Start(string.Format("https://dashboard.microsofthealth.com/#/{0}/{1}-{2}-{3}/month/chart", "exercise", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                                break;
                            case "Golf":
                                Process.Start(string.Format("https://dashboard.microsofthealth.com/#/{0}/{1}-{2}-{3}/month/chart", "golf", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                                break;
                            case "Guided Workout":
                                Process.Start(string.Format("https://dashboard.microsofthealth.com/#/{0}/{1}-{2}-{3}/month/chart", "guided", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                                break;
                            case "Run":
                                Process.Start(string.Format("https://dashboard.microsofthealth.com/#/{0}/{1}-{2}-{3}/month/chart", "run", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                                break;
                            case "Sleep":
                                Process.Start(string.Format("https://dashboard.microsofthealth.com/#/{0}/{1}-{2}-{3}/month/chart", "sleep", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                                break;
                        }
                    };

                    for(int j = 0; j < pInfoArr.Length - 3; j++)
                    {
                        if(vm.Activities[i].GetType() == typeof(GolfActivity) && j == 2)
                            toAdd = PrepActControls(pInfoArr[4].Name, pInfoArr[4].GetValue(vm.Activities[i]).ToString() ?? "null", (i < 3 ? i : i - 3), j, (i < 3 ? 0 : 1));
                        else if(vm.Activities[i].GetType() == typeof(SleepActivity) && j == 1)
                            toAdd = PrepActControls(pInfoArr[5].Name, pInfoArr[5].GetValue(vm.Activities[i]).ToString() ?? "null", (i < 3 ? i : i - 3), j, (i < 3 ? 0 : 1));
                        else if(vm.Activities[i].GetType() == typeof(SleepActivity) && j == 2)
                            toAdd = PrepActControls(pInfoArr[6].Name, pInfoArr[6].GetValue(vm.Activities[i]).ToString() ?? "null", (i < 3 ? i : i - 3), j, (i < 3 ? 0 : 1));
                        else
                            toAdd = PrepActControls(pInfoArr[j].Name, pInfoArr[j].GetValue(vm.Activities[i]).ToString() ?? "null", (i < 3 ? i : i - 3), j, (i < 3 ? 0 : 1));

                        grdAct.Children.Add((Label)toAdd[0]);
                        grdAct.Children.Add((TextBlock)toAdd[1]);
                        grdAct.Opacity = (i % 2 == 0 ? 1 : .4);
                    }

                    grdActivities.Children.Add(grdAct);
                }

                PageFadeIn(grdActivities);
            }
        }

        private async void lblDev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdDevices.Opacity == 0)
            {
                vm.Devices = await Repository.GetDevicesAsync();

                Label lblRedTitle = null;
                string[] titles = { "Id", "", "Sync", "Family", "Hardware", "Software", "Model", "Manufact.", "Status", "Created" };
                string[] fileNames = { "mobile.png", "sync.png", "house.png", "gear.png", "window.png", "computer.png", "suitcase.png", "sun.png", "globe.png" };
                object[] toAdd = new object[2];
                PropertyInfo[] pInfoArr = typeof(Device).GetProperties();

                for(int i = 0; i < grdDevices.Children.Count; i++)
                {
                    if(!(grdDevices.Children[i] is Image))
                    {
                        grdDevices.Children.RemoveAt(i);
                    }
                }

                for(int i = 0; i < vm.Devices.Count; i++)
                {
                    Grid grdDev = new Grid();
                    grdDev.Opacity = (i % 2 == 0 ? 1 : .4);
                    grdDevices.Children.Add(grdDev);

                    lblRedTitle = new Label();
                    lblRedTitle.Content = vm.Devices[i].DisplayName.ToUpper();
                    lblRedTitle.HorizontalAlignment = HorizontalAlignment.Left;
                    lblRedTitle.Margin = new Thickness(283 + i * 168, 97, 0, 0);
                    lblRedTitle.VerticalAlignment = VerticalAlignment.Top;
                    lblRedTitle.Foreground = new SolidColorBrush(new Color() { R = 255, G = 25, B = 43, A = 255 });
                    lblRedTitle.FontFamily = new FontFamily("Aller Display");
                    lblRedTitle.FontSize = 11;
                    grdDevices.Children.Add(lblRedTitle);

                    for(int j = 0; j < typeof(Device).GetProperties().Length; j++)
                    {
                        if(j == 1) continue;
                        string prop = pInfoArr[j].PropertyType == typeof(DateTime) ? ((DateTime)pInfoArr[j].GetValue(vm.Devices[i])).ToString("d") : pInfoArr[j].GetValue(vm.Devices[i]).ToString();
                        toAdd = PrepDevControls(titles[j], prop, i, j > 1 ? j - 1 : j);
                        grdDev.Children.Add((Label)toAdd[0]);
                        grdDev.Children.Add((TextBlock)toAdd[1]);
                        AddImg(grdDev, fileNames[j > 1 ? j - 1 : j], 8, 8, new Thickness(345 + (i * 168), 128 + ((j > 1 ? j - 1 : j) * 32), 447 - (i * 168), 330 - ((j > 1 ? j - 1 : j) * 32)));
                    }

                    AddImg(grdDevices, "top-corner.png", 8, 8, new Thickness(264 + (i * 168), 80, 528 - (i * 168), 378));
                    AddImg(grdDevices, "bottom-corner.png", 8, 8, new Thickness(425 + (i * 168), 417, 367 - (i * 168), 41));
                }

                PageFadeIn(grdDevices);
            }

        }

        private async void lblPro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdProfile.Opacity == 0)
            {
                lblPro.MouseDown -= lblPro_MouseDown;
                imgUser.MouseDown -= lblPro_MouseDown;
                vm.Profile = await Repository.GetProfileAsync();
                vm.OnPropertyChanged("Profile");
                vm.Profile.OnPropertyChanged("FirstName");
                lblPro.MouseDown += lblPro_MouseDown;
                imgUser.MouseDown += lblPro_MouseDown;
                PageFadeIn(grdProfile);
            }
        }

        private async void lblSum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(grdSummaries.Opacity == 0)
            {
                grdSummaries.Children.RemoveRange(4, grdSummaries.Children.Count - 5);

                vm.Summaries = await Repository.GetSummariesAsync();
                PropertyInfo[] pInfoArr = typeof(IActivity).GetProperties();
                object[] toAdd = new object[2];
                string[] fileNames = { "clock-dark.png", "sync-dark.png", "anchor-dark.png", "alarm-dark.png" };

                AddImg(grdSummaries, "top-corner.png", 8, 8, new Thickness(264, 80 + 172, 528, 378 - 172));
                AddImg(grdSummaries, "bottom-corner.png", 8, 8, new Thickness(425 + (2 * 168), 417, 367 - (2 * 168), 41));
                AddImg(grdSummaries, "top-corner.png", 8, 8, new Thickness(264, 80, 528, 378));
                AddImg(grdSummaries, "bottom-corner.png", 8, 8, new Thickness(425 + (2 * 168), 417 - 172, 367 - (2 * 168), 41 + 172));
                Image hyperlink;

                (hyperlink = AddImg(grdSummaries, "hyperlink.png", 8, 8, new Thickness(408 + (2 * 168), 104, 384 - (2 * 168), 354))).MouseDown += (s, eArgs) => {
                    Process.Start("https://dashboard.microsofthealth.com/#/compare/");
                };

                Label lblRedTitle = new Label();
                lblRedTitle.Content = "Summaries".ToUpper();
                lblRedTitle.HorizontalAlignment = HorizontalAlignment.Left;
                lblRedTitle.Margin = new Thickness(283, 97, 0, 0);
                lblRedTitle.VerticalAlignment = VerticalAlignment.Top;
                lblRedTitle.Foreground = new SolidColorBrush(new Color() { R = 255, G = 25, B = 43, A = 255 });
                lblRedTitle.FontFamily = new FontFamily("Aller Display");
                lblRedTitle.FontSize = 11;
                grdSummaries.Children.Add(lblRedTitle);

                for(int i = 0; i < pInfoArr.Length - 3; i++)
                {
                    string key = pInfoArr[i].Name;
                    string value = pInfoArr[i].GetValue(vm.Summaries).ToString() ?? "null";

                    AddImg(grdSummaries, fileNames[i], 8, 8, new Thickness(520, 126 + i * 32, 272, 328 - i * 32));

                    switch(key)
                    {
                        case "Period":
                            value = (DateTime.Now - TimeSpan.Parse(value)).ToString("y");
                            break;
                        case "TotalDistance":
                            value = string.Format("{0} m", value);
                            key = "Distance";
                            break;
                        case "Speed":
                            value = string.Format("{0} Km/h", value);
                            break;
                        case "AverageHeartRate":
                            value = string.Format("{0} per m", value);
                            key = "Heart Rate";
                            break;
                        case "HoleNumber":
                            key = "Hole Nr.";
                            break;
                        case "Duration":
                            value = string.Format("{0} h", value);
                            break;
                        case "StartTime":
                            value = DateTime.Parse(value).ToString("t");
                            key = "Time";
                            break;
                    }

                    Label lbl = new Label();
                    lbl.Content = key;
                    lbl.HorizontalAlignment = HorizontalAlignment.Left;
                    lbl.Margin = new Thickness(283, 123 + i * 32, 0, 0);
                    lbl.VerticalAlignment = VerticalAlignment.Top;
                    lbl.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
                    lbl.FontFamily = new FontFamily("Nobile");
                    lbl.FontSize = 10;
                    toAdd[0] = lbl;

                    TextBlock tbk = new TextBlock();
                    tbk.Text = value;
                    tbk.HorizontalAlignment = HorizontalAlignment.Left;
                    tbk.Margin = new Thickness(316 + 2 * 168, 128 + i * 32, 0, 0);
                    tbk.VerticalAlignment = VerticalAlignment.Top;
                    tbk.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
                    tbk.FontFamily = new FontFamily("Nobile");
                    tbk.FontSize = 10;
                    tbk.Width = 100;
                    tbk.TextAlignment = TextAlignment.Right;
                    toAdd[1] = tbk;

                    grdSummaries.Children.Add((Label)toAdd[0]);
                    grdSummaries.Children.Add((TextBlock)toAdd[1]);
                }

                DrawGraph();

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

                AddImg(grdDevices, "top-corner.png", 8, 8, new Thickness(264 + vm.Devices.Count * 168, 80, 528 - vm.Devices.Count * 168, 378));
                AddImg(grdDevices, "bottom-corner.png", 8, 8, new Thickness(425 + vm.Devices.Count * 168, 417, 367 - vm.Devices.Count * 168, 41));
                Image imgDiscartHover = AddImg(addGrid, "discart-hover.png", 8, 8, new Thickness(296 + vm.Devices.Count * 168, 102, 480 - vm.Devices.Count * 168, 348));
                Image imgDiscart;
                (imgDiscart = AddImg(addGrid, "discart.png", 8, 8, new Thickness(296 + vm.Devices.Count * 168, 102, 480 - vm.Devices.Count * 168, 348), (Style)TryFindResource("ImgFadeOut"))).MouseDown += (s, eArgs) =>
                {
                    grdDevices.Children.Remove(addGrid);
                    imgAddDev.MouseDown += imgAddDev_MouseDown;
                };
                Image imgUploadHover =AddImg(addGrid, "upload-hover.png", 8, 8, new Thickness(284 + vm.Devices.Count * 168, 102, 500 - vm.Devices.Count * 168, 348));
                Image imgUpload;
                (imgUpload = AddImg(addGrid, "upload.png", 8, 8, new Thickness(284 + vm.Devices.Count * 168, 102, 500 - vm.Devices.Count * 168, 348), (Style)TryFindResource("ImgFadeOut"))).MouseDown += async (s, eArgs) => 
                {
                    Device temp = new Device();

                    for(int i = 0; i < addGrid.Children.OfType<Label>().Count(); i++)
                    {
                        if(addGrid.Children.OfType<Label>().ElementAt<Label>(i).Content.ToString() == "Name")
                        {
                            addGrid.Children.OfType<Label>().ElementAt<Label>(i).Content = "Id";
                            break;
                        }
                    }

                    temp.DisplayName = addGrid.Children.OfType<TextBox>().ElementAt(0).Text.Trim();
                    DateTime tempDate;
                    DateTime.TryParse(addGrid.Children.OfType<TextBox>().ElementAt(1).Text.Trim(), vm.Profile.PreferredLocale, System.Globalization.DateTimeStyles.None, out tempDate);
                    temp.LastSuccessfulSync = tempDate;
                    DeviceFamily tempFamily;
                    Enum.TryParse<DeviceFamily>(addGrid.Children.OfType<TextBox>().ElementAt(2).Text.Trim(), out tempFamily);
                    temp.DeviceFamily = tempFamily;
                    temp.HardwareVersion = addGrid.Children.OfType<TextBox>().ElementAt(3).Text.Trim();
                    temp.SoftwareVersion = addGrid.Children.OfType<TextBox>().ElementAt(4).Text.Trim();
                    temp.ModelName = addGrid.Children.OfType<TextBox>().ElementAt(5).Text.Trim();
                    temp.Manufacturer = addGrid.Children.OfType<TextBox>().ElementAt(6).Text.Trim();
                    DeviceStatus tempStatus;
                    Enum.TryParse<DeviceStatus>(addGrid.Children.OfType<TextBox>().ElementAt(7).Text.Trim(), out tempStatus);
                    temp.DeviceStatus = tempStatus;
                    DateTime.TryParse(addGrid.Children.OfType<TextBox>().ElementAt(8).Text.Trim(), vm.Profile.PreferredLocale, System.Globalization.DateTimeStyles.None, out tempDate);
                    temp.CreatedDate = tempDate;

                    if(Validate(addGrid))
                    {
                        await Repository.AddDeviceAsync(temp);
                        imgAddDev.MouseDown += imgAddDev_MouseDown;

                        for(int i = 0; i < addGrid.Children.Count; i++)
                        {
                            if((addGrid.Children[i] is TextBox))
                            {
                                addGrid.Children.RemoveAt(i);
                            }
                        }

                        addGrid.Children.Remove(imgUploadHover);
                        addGrid.Children.Remove(imgUpload);
                        addGrid.Children.Remove(imgDiscartHover);
                        addGrid.Children.Remove(imgDiscart);

                        vm.Devices = await Repository.GetDevicesAsync();

                        Label lblRedTitle = null;
                        string[] fileNames = { "mobile.png", "sync.png", "house.png", "gear.png", "window.png", "computer.png", "suitcase.png", "sun.png", "globe.png" };
                        PropertyInfo[] pInfoArr = typeof(Device).GetProperties();
                        int k = vm.Devices.Count - 1;

                        lblRedTitle = new Label();
                        lblRedTitle.Content = vm.Devices[k].DisplayName.ToUpper();
                        lblRedTitle.HorizontalAlignment = HorizontalAlignment.Left;
                        lblRedTitle.Margin = new Thickness(283 + k * 168, 97, 0, 0);
                        lblRedTitle.VerticalAlignment = VerticalAlignment.Top;
                        lblRedTitle.Foreground = new SolidColorBrush(new Color() { R = 255, G = 25, B = 43, A = 255 });
                        lblRedTitle.FontFamily = new FontFamily("Aller Display");
                        lblRedTitle.FontSize = 11;
                        grdDevices.Children.Add(lblRedTitle);

                        for(int j = 0; j < typeof(Device).GetProperties().Length; j++)
                        {
                            if(j == 1) continue;
                            string prop = pInfoArr[j].PropertyType == typeof(DateTime) ? ((DateTime)pInfoArr[j].GetValue(vm.Devices[k])).ToString("d") : pInfoArr[j].GetValue(vm.Devices[k]).ToString();
                            toAdd = PrepDevControls(typeof(Device).GetProperties()[j].Name, prop, k, j > 1 ? j - 1 : j);
                            addGrid.Children.Add((TextBlock)toAdd[1]);
                            AddImg(addGrid, fileNames[j > 1 ? j - 1 : j], 8, 8, new Thickness(345 + (k * 168), 128 + ((j > 1 ? j - 1 : j) * 32), 447 - (k * 168), 330 - ((j > 1 ? j - 1 : j) * 32)));
                        }

                        addGrid.Opacity = (k % 2 == 0 ? 1 : .4);
                    }
                };

                grdDevices.Children.Add(addGrid);
                imgAddDev.MouseDown -= imgAddDev_MouseDown;
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

        private object[] PrepActControls(string key, string value, int col, int row, int r)
        {
            object[] result = new object[2];

            switch(key)
            {
                case "Period": value = (DateTime.Now - TimeSpan.Parse(value)).ToString("y");
                    break;
                case "TotalDistance":
                    value = string.Format("{0} m", value);
                    key = "Distance";
                    break;
                case "Speed":
                    value = string.Format("{0} Km/h", value);
                    break;
                case "AverageHeartRate":
                    value = string.Format("{0} per m", value);
                    key = "Heart Rate";
                    break;
                case "HoleNumber":
                    key = "Hole Nr.";
                    break;
                case "Duration":
                    value = string.Format("{0} h", value);
                    break;
                case "StartTime":
                    value = DateTime.Parse(value).ToString("t");
                    key = "Time";
                    break;
            }

            Label lbl = new Label();
            lbl.Content = key;
            lbl.HorizontalAlignment = HorizontalAlignment.Left;
            lbl.Margin = new Thickness(283 + col * 168, 123 + row * 32 + (r * 172), 0, 0);
            lbl.VerticalAlignment = VerticalAlignment.Top;
            lbl.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
            lbl.FontFamily = new FontFamily("Nobile");
            lbl.FontSize = 10;
            result[0] = lbl;

            TextBlock tbk = new TextBlock();
            tbk.Text = value;
            tbk.HorizontalAlignment = HorizontalAlignment.Left;
            tbk.Margin = new Thickness(316 + col * 168, 128 + row * 32 + (r * 172), 0, 0);
            tbk.VerticalAlignment = VerticalAlignment.Top;
            tbk.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
            tbk.FontFamily = new FontFamily("Nobile");
            tbk.FontSize = 10;
            tbk.Width = 100;
            tbk.TextAlignment = TextAlignment.Right;
            result[1] = tbk;

            return result;
        }

        private object[] PrepDevControls(string key, string value, int col, int row)
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

            TextBlock tbk = new TextBlock();
            tbk.Text = value;
            tbk.HorizontalAlignment = HorizontalAlignment.Left;
            tbk.Margin = new Thickness(316 + col * 168, 128 + row * 32, 0, 0);
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
            txt.Margin = new Thickness(346 + col * 168, 124 + row * 32, 0, 0);
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

        private void RefreshPage(Grid g)
        {
            ((Storyboard)FindResource("PageFadeOut")).Begin(g);
            PageFadeIn(g);
        }

        private bool Validate(Grid g)
        {
            String message = string.Empty;
            TextBox[] ctrls = g.Children.OfType<TextBox>().ToArray<TextBox>();

            foreach(TextBox element in ctrls)
            {
                if(element.Text == string.Empty)
                {
                    message += "This form is not completed." + Environment.NewLine;
                    break;
                }
            }

            DateTime tempDate;

            if(!DateTime.TryParse(ctrls[1].Text, out tempDate) || !DateTime.TryParse(ctrls[8].Text, out tempDate))
            {
                message += "This is not a valid date format (dd.mm.yyyy)." + Environment.NewLine;
            }

            DeviceFamily tempFamily;

            if(!Enum.TryParse<DeviceFamily>(ctrls[2].Text, out tempFamily))
            {
                message += "The Family field can only have the values Mobile, Tablet and Desktop." + Environment.NewLine;
            }

            DeviceStatus tempStatus;

            if(!Enum.TryParse<DeviceStatus>(ctrls[7].Text, out tempStatus))
            {
                message += "The Device Status field can only have the values On and Off." + Environment.NewLine;
            }

            if(message != string.Empty)
            {
                MessageBox.Show(message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void DrawGraph()
        {
            cnvVisual.Children.Clear();

            Label lblRedTitle = new Label();
            lblRedTitle.Content = "This Month".ToUpper();
            lblRedTitle.HorizontalAlignment = HorizontalAlignment.Left;
            lblRedTitle.Margin = new Thickness(283, 97 + 172, 0, 0);
            lblRedTitle.VerticalAlignment = VerticalAlignment.Top;
            lblRedTitle.Foreground = new SolidColorBrush(new Color() { R = 255, G = 25, B = 43, A = 255 });
            lblRedTitle.FontFamily = new FontFamily("Aller Display");
            lblRedTitle.FontSize = 11;
            grdSummaries.Children.Add(lblRedTitle);

            SolidColorBrush yellow = new SolidColorBrush();
            yellow.Color = new Color() { R = 254, G = 255, B = 130, A = 255 };

            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = new Color() { R = 0, G = 166, B = 255, A = 255 };

            int tempX = 0;
            int tempY = 120;

            for(int i = 0; i < 32; i++)
            {
                Thread.Sleep(10);

                System.Windows.Shapes.Line yellowLine = new System.Windows.Shapes.Line();
                yellowLine.X1 = tempX;
                yellowLine.Y1 = tempY;
                yellowLine.X2 = tempX + new Random().Next(40);
                yellowLine.Y2 = 120 - new Random().Next(40);

                yellowLine.StrokeThickness = 1;
                yellowLine.Stroke = yellow;

                cnvVisual.Children.Add(yellowLine);

                tempX = (int)yellowLine.X2;
                tempY = (int)yellowLine.Y2;
            }

            tempX = 0;
            tempY = 120;

            for(int i = 0; i < 32; i++)
            {
                Thread.Sleep(10);

                System.Windows.Shapes.Line blueLine = new System.Windows.Shapes.Line();
                blueLine.X1 = tempX;
                blueLine.Y1 = tempY;
                blueLine.X2 = tempX + new Random().Next(56);
                blueLine.Y2 = 120 - new Random().Next(56);

                blueLine.StrokeThickness = 1;
                blueLine.Stroke = blue;

                cnvVisual.Children.Add(blueLine);

                tempX = (int)blueLine.X2;
                tempY = (int)blueLine.Y2;
            }
        }
    }
}
