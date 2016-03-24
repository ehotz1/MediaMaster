using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Forms;
using System.IO;

namespace MediaMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string movieDir = Properties.Settings.Default.MovieDir;
        private string showDir = Properties.Settings.Default.ShowDir;
        private string musicDir = Properties.Settings.Default.MusicDir;
        private string photoDir = Properties.Settings.Default.PhotoDir;

        public MainWindow()
        {
            
            
            InitializeComponent();
        }

        private void MovieDir_Click(object sender, RoutedEventArgs e)
        {
            //Open File Explorer, select directory
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if ( result == System.Windows.Forms.DialogResult.OK )
            {
                movieDir = dialog.SelectedPath;
                Properties.Settings.Default.MovieDir = movieDir;
            }
        }

        

        private void ShowDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                showDir = dialog.SelectedPath;
                Properties.Settings.Default.ShowDir = showDir;
                
            }
        }

        private void MusicDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                musicDir = dialog.SelectedPath;
                Properties.Settings.Default.MusicDir = musicDir;
            }
        }

        private void Movies_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Movies window = new Movies(movieDir, this);
            window.Show();

        }

        private void Shows_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Shows window = new Shows(showDir, this);
            window.Show();
        }

        private void Music_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Music window = new Music(musicDir, this);
            window.Show();
        }

        private void Photos_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Photos window = new Photos(photoDir, this);
            window.Show();
        }

        private void PhotoDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                photoDir = dialog.SelectedPath;
                Properties.Settings.Default.PhotoDir = photoDir;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
