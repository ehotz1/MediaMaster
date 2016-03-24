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
    public partial class Movies : Window
    {
        private string directory;
        private int index;
        private MovieList movies;
        private MainWindow parent;
        private ObservableCollection<Movie> mov_list;

        public Movies(string dir, MainWindow parent)
        {
            this.directory = dir;
            movies = new MovieList(directory);
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
                MovieModel model = this.Resources["model"] as MovieModel;
                this.movies.Initialize(model);
                mov_list = model.Movies;
                MovieList.SelectedIndex = 0;
                
            } catch { Console.WriteLine("Resource Error"); }
        }

        

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            index = MovieList.SelectedIndex;
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
            index = MovieList.SelectedIndex;
            MediaWindow media = new MediaWindow(this, index, GetFilePaths(),playlist);
            media.Show();
            this.Hide();
        }

        private ObservableCollection<Uri> GetFilePaths()
        {
            ObservableCollection<Uri> paths = new ObservableCollection<Uri>();
            foreach (Movie mov in mov_list)
            {
                paths.Add(mov.FilePath);
            }
            return paths;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            parent.Show();
        }

        private void MovieList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
