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
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using Emgu.CV.Util;
using Emgu.CV.Features2D;

namespace Photonshop_WPF

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage bm = new BitmapImage();
        private Mat pic = new Mat();
        private string fimeName = "";
        public MainWindow()
        {
            InitializeComponent();
            bm.BeginInit();
            bm.UriSource = new Uri("C:\\Users\\pappg\\my stuff\\Code Repos\\Photonshop\\Photonshop WPF\\shrek.jpg");
            bm.EndInit();
            RefreshImage();

            pic = CvInvoke.Imread("C:\\Users\\pappg\\my stuff\\Code Repos\\Photonshop\\Photonshop WPF\\shrek.jpg");

            
        }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            pic = GaussBlur(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }

        private void Button_invert(object sender, RoutedEventArgs e)
        {
            pic = Invert(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_log(object sender, RoutedEventArgs e)
        {
            //TODO
            //pic =LogImage(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_gray(object sender, RoutedEventArgs e)
        {
            pic =GrayScale(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_histogram(object sender, RoutedEventArgs e)
        {
            //TODO
            //pic =Histogram(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_HE(object sender, RoutedEventArgs e)
        {
            //TODO
            //pic =;
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_box(object sender, RoutedEventArgs e)
        {
            pic =BoxFilter(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_sobel(object sender, RoutedEventArgs e)
        {
            pic =Sobel(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_laplace(object sender, RoutedEventArgs e)
        {
            pic =Laplace(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
        }
        private void Button_CH(object sender, RoutedEventArgs e)
        {
            pic =CHPoints(pic);
            bm = ConvertBitmapToBitmapImage(pic.ToBitmap());
            RefreshImage();
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
                fimeName = selectedFileName;
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
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        private Mat GaussBlur(Mat pic)
        {
            Mat result = new Mat();
            CvInvoke.GaussianBlur(pic, result, new System.Drawing.Size(pic.Cols - 1, pic.Rows - 1), 5.0);
            return result;
        }

        private Mat Invert(Mat pic)
        {
            Mat result = new Mat();
            CvInvoke.BitwiseNot(pic, result);
            return result;
        }

        /*private Mat LogImage(Mat pic)
        {
            Mat result = new Mat();

            Mat floatImage = new Mat();
            pic.ConvertTo(floatImage, DepthType.Cv32F);
            
            Mat onesMatrix = new Mat.Ones(floatImage.Size, DepthType.Cv32F);

            // Step 3: Add 1 to each pixel to avoid taking log(0)
            CvInvoke.Add(floatImage, onesMatrix, floatImage);

            // Step 4: Apply the logarithmic transformation
            Mat logImage = new Mat();
            CvInvoke.Log(floatImage, logImage);

            // Step 5: Normalize to bring back to displayable range (0 - 255)
            CvInvoke.Normalize(logImage, logImage, 0, 255, NormType.MinMax);

            // Step 6: Convert back to 8-bit to save or display
            Mat outputImage = new Mat();
            logImage.ConvertTo(result, DepthType.Cv8U);

            return result;
        }*/

        private Mat GrayScale( Mat pic)
        {
            Mat result = new Mat();
            CvInvoke.CvtColor(pic, result, ColorConversion.Bgr2Gray);
            return result;
        }

        /*private Mat Histogram(Mat pic)
        {
            Mat result = new Mat();
            CvInvoke.Split(pic, channels);

            int histsize = 256;

            float[] range = new float[] { 0, 256 };

            CvInvoke.CalcHist


            return result;
        }*/

        private Mat BoxFilter(Mat pic)
        {
            Mat result = new Mat();

            CvInvoke.BoxFilter(pic, result, DepthType.Cv8U, new System.Drawing.Size(5, 5), new System.Drawing.Point(-1, -1), true, BorderType.Default);


            return result;
        }

        private Mat Sobel(Mat pic)
        {
            Mat result = new Mat();
            Mat grayImage = GrayScale(pic);
            Mat gradX = new Mat();
            Mat gradY = new Mat();

            CvInvoke.Sobel(grayImage, gradX, DepthType.Cv8U, 1, 0, 3, 1, 0, BorderType.Reflect);
            CvInvoke.Sobel(grayImage, gradY, DepthType.Cv8U, 0, 1, 3, 1, 0, BorderType.Reflect);

            CvInvoke.AddWeighted(gradX, 0.5, gradY, 0.5, 0, result);

            return result;
        }

        private Mat Laplace (Mat pic)
        {
            Mat result = new Mat();
            Mat grayImage = GrayScale(pic);
            Mat laplaceImage = new Mat();

            CvInvoke.Laplacian(grayImage, laplaceImage, DepthType.Cv8U, 3, 1, 0, BorderType.Reflect);
            CvInvoke.AbsDiff(laplaceImage, new UMat(laplaceImage.Size, DepthType.Cv8U, 1), result);

            return result;
        }

        private Mat CHPoints (Mat pic)
        {
            Mat result = new Mat();
            Mat grayImage = GrayScale(pic);
            Mat descriptors = new Mat();

            var brisk = new Brisk();

            VectorOfKeyPoint keyPoint = new VectorOfKeyPoint();
            brisk.DetectAndCompute(grayImage, null, keyPoint, descriptors, false);

            Features2DToolbox.DrawKeypoints(pic, keyPoint, result, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.DrawRichKeypoints);

            return result;
        }

        private void RefreshImage()
        {
            imageViewer.Source = bm;
        }

    }
}