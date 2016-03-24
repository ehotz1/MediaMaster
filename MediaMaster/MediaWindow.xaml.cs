using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace MediaMaster
{
    /// <summary>
    /// Interaction logic for Media.xaml
    /// </summary>
    public partial class MediaWindow : Window
    {
        Window parent;
        ObservableCollection<Uri> list;
        int index;
        bool fullscreen;
        bool playing;
        bool playlist;

        public MediaWindow(Window parent, int index, ObservableCollection<Uri> collection, bool playlist)
        {
            this.parent = parent;
            this.index = index;
            list = collection;
            this.playlist = playlist;
            InitializeComponent();
            this.DataContext = this;
        }

        public Uri Source
        {
            get { return list[index]; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullScreen();
            fullscreen = true;
            playing = true;
        }

        private void FullScreen()
        {
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            fullscreen = true;
        }

        private void Minimize()
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = WindowState.Normal;
            fullscreen = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F)
            {
                if (!fullscreen) { FullScreen(); } else { Minimize(); }
            }

            if (e.Key == Key.Escape)
            {
                Minimize();
            }

            if (e.Key == Key.Space)
            {
                if (playing)
                {
                    player.Pause();
                    playing = false;
                } else
                {
                    player.Play();
                    playing = true;
                }
            }

            if (e.Key == Key.Right)
            {
                FastForward();
            }

            if (e.Key == Key.Left)
            {
                Rewind();
            }

            if (e.Key == Key.E)
            {
                SkipForward();
            }

            if (e.Key == Key.Q)
            {
                SkipBackward();
            }
        }

        private void FastForward()
        {
            player.Position = player.Position.Add(new TimeSpan(0, 0, 2));
        }

        private void Rewind()
        {
            player.Position = player.Position.Add(new TimeSpan(0, 0, -2));
        }

        private void SkipForward()
        {
            player.Position = player.Position.Add(new TimeSpan(0, 1, 0));
        }

        private void SkipBackward()
        {
            player.Position = player.Position.Add(new TimeSpan(0, -1, 0));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Show();
            player.Stop();
            this.Close();
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
