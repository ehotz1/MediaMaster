using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaMaster
{
    public class MovieList
    {
        private DirectoryInfo dirInfo;
        private string[] extensions = { ".mp4", ".wmv", ".avi" };
        private MovieModel model;
        private ObservableCollection<Movie> list;

        

        public MovieList(string directory)
        {
            this.Directory = directory;
            list = new ObservableCollection<Movie>();
            PopulateList();
        }

        public void Initialize(MovieModel model)
        {
            this.model = model;
            this.model.Movies = list;
        }

        public string Directory
        {
            set
            {
                try {
                    dirInfo = new DirectoryInfo(value);
                } catch
                {
                    Console.WriteLine("No directory selected");
                }
                
            }
            get { return dirInfo.FullName; }
        }



        private void PopulateList()
        {
            try {
                IEnumerable<FileInfo> FileList = GetFiles(dirInfo, extensions);
                //string[] folders = Directory.GetDirectories(dir);
                foreach (FileInfo file in FileList)
                {
                    list.Add(new Movie(file.FullName, file.Name));
                }
            } catch { }
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo direct, params string[] extensions)
        {
            return direct.EnumerateFiles().Where(f => extensions.Contains(f.Extension));
        }
    }

    public class MovieModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Movie> _movies;

        public ObservableCollection<Movie> Movies
        {
            get { return this._movies; }
            set
            {
                this._movies = value;
                this.sendPropertyChanged("Movies");
            }
        }

        private void sendPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }

    public class Movie
    {
        private string name;
        private string path;
        private Uri source;

        public Movie(string path)
        {
            this.path = path;
            source = new Uri(path);
        }

        public Movie(string path, string name)
        {
            this.name = name;
            this.path = path;
            source = new Uri(path);
        }

        public string Title
        {
            get { return name; }
            set
            {
                if (name != value)
                    name = value;
            }
        }

        public string Source
        {
            get { return path; }
        }

        public Uri FilePath
        {
            get { return source; }
        }
    }
}
