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
    public class ShowList
    {
        private DirectoryInfo dirInfo;
        private string[] extensions = { ".mp4", ".wmv", ".avi" };
        private ShowModel model;
        private ObservableCollection<Show> list;

        

        public ShowList(string directory)
        {
            this.Directory = directory;
            list = new ObservableCollection<Show>();
            PopulateList();
        }

        public void Initialize(ShowModel model)
        {
            this.model = model;
            this.model.Shows = list;
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
                    list.Add(new Show(file.FullName, file.Name));
                }
            } catch { }
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo direct, params string[] extensions)
        {
            return direct.EnumerateFiles().Where(f => extensions.Contains(f.Extension));
        }
    }

    public class ShowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Show> shows;

        public ObservableCollection<Show> Shows
        {
            get { return this.shows; }
            set
            {
                this.shows = value;
                this.sendPropertyChanged("Shows");
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

    public class Show
    {
        private string name;
        private string path;
        private Uri source;

        public Show(string path)
        {
            this.path = path;
            source = new Uri(path);
        }

        public Show(string path, string name)
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
