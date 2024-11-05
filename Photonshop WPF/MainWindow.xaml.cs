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

namespace Photonshop_WPF

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bm = new BitmapImage();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            bm.BeginInit();
            bm.UriSource = new Uri("C:\\Users\\pappg\\my stuff\\Code Repos\\Photonshop\\Photonshop WPF\\bin\\Debug\\net8.0-windows\\shrek.jpg");
            bm.EndInit();
            imageViewer.Source = bm;
        }
    }
}