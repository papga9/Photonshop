using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV;
using Emgu.CV.Cuda;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Photonshop_WPF

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bm = new BitmapImage();
        Mat pic = new Mat();
        public MainWindow()
        {
            InitializeComponent();
            bm.BeginInit();
            bm.UriSource = new Uri("shrek.jpg", UriKind.Relative);
            bm.EndInit();
            RefreshImage();

            pic = CvInvoke.Imread("shrek.jpg");

            
        }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            bm = ConvertBitmapToBitmapImage(GaussBlur(pic).ToBitmap());
            RefreshImage();
        }

        private Mat GaussBlur (Mat pic)
        {
            Mat result = new Mat();
            System.Console.WriteLine(pic.Rows);
            CvInvoke.GaussianBlur(pic, result, new System.Drawing.Size(pic.Cols-1, pic.Rows-1), 5.0);
                return result;
        }

        private void Button_browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true )
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                bm = bitmap;
                pic = pic = CvInvoke.Imread(selectedFileName);
                RefreshImage();
            }
        }


        public BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the Bitmap to the MemoryStream
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                // Reset the stream position to the beginning
                memoryStream.Position = 0;

                // Create a BitmapImage and load the MemoryStream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void RefreshImage()
        {
            imageViewer.Source = bm;
        }

    }
}