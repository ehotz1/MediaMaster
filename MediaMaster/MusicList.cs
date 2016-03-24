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
    public class MusicList
    {
        private DirectoryInfo dirInfo;
        private string[] extensions = { ".mp3", ".wma", ".wav" };
        private MusicModel model;
        private ObservableCollection<Song> list;

        

        public MusicList(string directory)
        {
            this.Directory = directory;
            list = new ObservableCollection<Song>();
            PopulateList();
        }

        public void Initialize(MusicModel model)
        {
            this.model = model;
            this.model.Songs = list;
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
                foreach (FileInfo file in FileList)
                {
                    list.Add(new Song(file.FullName, file.Name));
                }
            } catch { }
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo direct, params string[] extensions)
        {
            return direct.EnumerateFiles().Where(f => extensions.Contains(f.Extension));
        }
    }

    public class MusicModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Song> songs;

        public ObservableCollection<Song> Songs
        {
            get { return this.songs; }
            set
            {
                this.songs = value;
                this.sendPropertyChanged("Songs");
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

    public class Song
    {
        private string name;
        private string path;
        private Uri source;

        public Song(string path)
        {
            this.path = path;
            source = new Uri(path);
        }

        public Song(string path, string name)
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
