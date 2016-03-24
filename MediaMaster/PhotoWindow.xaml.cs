using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PhotoWindow.xaml
    /// </summary>
    public partial class PhotoWindow : Window
    {
        Window parent;
        
        ObservableCollection<Uri> list;
        int index;
        bool fullscreen;
        
        

        public PhotoWindow(Window parent, int index, ObservableCollection<Uri> collection)
        {
            this.parent = parent;
            this.index = index;
            list = collection;
            
            
            InitializeComponent();
            this.DataContext = this;
        }

        public Uri Source
        {
            get { return list[index]; }
            set
            {
                list[index] = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullScreen();
            fullscreen = true;
            
            
            
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
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Show();
            this.Close();
        }

        
    }
}
