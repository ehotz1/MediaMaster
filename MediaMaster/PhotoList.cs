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
    public class PhotoList
    {
        private DirectoryInfo dirInfo;
        private string[] extensions = { ".jpg", ".png" };
        private PhotoModel model;
        private ObservableCollection<Photo> list;

        

        public PhotoList(string directory)
        {
            this.Directory = directory;
            list = new ObservableCollection<Photo>();
            PopulateList();
        }

        public void Initialize(PhotoModel model)
        {
            this.model = model;
            this.model.Photos = list;
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
                    list.Add(new Photo(file.FullName, file.Name));
                }
            } catch { }
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo direct, params string[] extensions)
        {
            return direct.EnumerateFiles().Where(f => extensions.Contains(f.Extension));
        }
    }

    public class PhotoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Photo> photos;

        public ObservableCollection<Photo> Photos
        {
            get { return this.photos; }
            set
            {
                this.photos = value;
                this.sendPropertyChanged("Photos");
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

    public class Photo
    {
        private string name;
        private string path;
        private Uri source;

        public Photo(string path)
        {
            this.path = path;
            source = new Uri(path);
        }

        public Photo(string path, string name)
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
