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
using System.Windows.Shapes;
using System.Threading;
using Lib;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace HealthyTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
            if (grdActivities.Opacity == 0)
            {
                PageFadeIn(grdActivities);
            }
        }

        private void lblDev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grdDevices.Opacity == 0)
            {
                PageFadeIn(grdDevices);
            }

        }

        private void lblPro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grdProfile.Opacity == 0)
            {
                PageFadeIn(grdProfile);
            }
        }

        private void lblSum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grdSummaries.Opacity == 0)
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
            if (grdHome.Opacity == 0)
            {
                PageFadeIn(grdHome);
            }
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
    }
}
