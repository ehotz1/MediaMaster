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
    /// Interaction logic for Photos.xaml
    /// </summary>
    public partial class Photos : Window
    {
        private string directory;
        private int index;
        private PhotoList photos;
        private MainWindow parent;
        private ObservableCollection<Photo> photo_list;
        

        public Photos(string dir, MainWindow parent)
        {
            this.directory = dir;
            photos = new PhotoList(directory);
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
                PhotoModel model = this.Resources["model"] as PhotoModel;
                this.photos.Initialize(model);
                photo_list = model.Photos;
                PhotoList.SelectedIndex = 0;
                
            } catch { Console.WriteLine("Resource Error"); }
        }

        

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            index = PhotoList.SelectedIndex;
            Play();
        }

        private void Play()
        {
            
            
            index = PhotoList.SelectedIndex;
            
            PhotoWindow window = new PhotoWindow(this, index, GetFilePaths());
            window.Show();
            this.Hide();

        }

        private ObservableCollection<Uri> GetFilePaths()
        {
            ObservableCollection<Uri> paths = new ObservableCollection<Uri>();
            foreach (Photo pic in photo_list)
            {
                paths.Add(pic.FilePath);
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
