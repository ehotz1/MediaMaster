using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace MediaMaster
{
    /// <summary>
    /// Interaction logic for Movies.xaml
    /// </summary>
    public partial class Music : Window
    {
        private string directory;
        private int index;
        private MusicList music;
        private MainWindow parent;
        private ObservableCollection<Song> list;

        public Music(string dir, MainWindow parent)
        {
            this.directory = dir;
            music = new MusicList(directory);
            this.parent = parent;
            
            InitializeComponent();
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (directory == null || directory == "")
            {
                MessageBox.Show("Please choose a directory.");
            }
            try
            {
                MusicModel model = this.Resources["model"] as MusicModel;
                this.music.Initialize(model);
                list = model.Songs;
                MusicList.SelectedIndex = 0;
                
            } catch { Console.WriteLine("Resource Error"); }
        }

        

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            index = MusicList.SelectedIndex;
            Play();
        }

        private void Play()
        {
            bool playlist;
            if (checkBox.IsChecked.Equals(true))
            {
                playlist = true;
            } else
            {
                playlist = false;
            }
            
            index = MusicList.SelectedIndex;
            MediaWindow media = new MediaWindow(this, index, GetFilePaths(),playlist);
            media.Show();
            this.Hide();
        }

        private ObservableCollection<Uri> GetFilePaths()
        {
            ObservableCollection<Uri> paths = new ObservableCollection<Uri>();
            foreach (Song song in list)
            {
                paths.Add(song.FilePath);
            }
            return paths;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            parent.Show();
        }

        
    }
}
