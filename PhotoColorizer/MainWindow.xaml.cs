using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Algorithmia;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace PhotoColorizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window


    {


        public MainWindow()
        {
            InitializeComponent();
        }



        public static void LoadImage()
        {
            //this is a method used to load the local image into memory that will be used for the algorithm


        }

        public static void PicColorize()

        {
            JObject APIkey = JObject.Parse(File.ReadAllText(@"C:\Users\plgon\source\repos\PhotoColorizer\PhotoColorizer\APIkey.json"));

            var input = "{"
                + "  \"image\": \"data://deeplearning/example_data/lincoln.jpg\"" // this should be changed to the image being loaded
                + "}";
            var client = new Client((string)APIkey["APIkey"]); //change the API for it to be loaded from a JSON file not exposed to Github
            var algorithm = client.algo("deeplearning/ColorfulImageColorization/1.1.13");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(input);
            Console.WriteLine(response.result);
            
        }

        public static void PicUploader() //uploads the picture to the algoritmia server
        {



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //JObject APIkey = JObject.Parse(File.ReadAllText(@"C:\Users\plgon\source\repos\PhotoColorizer\PhotoColorizer\APIkey.json"));
            //var client = new Client((string)APIkey["APIkey"]); //change the API for it to be loaded from a JSON file not exposed to Github
            //var colorizer = client.dir("data://plgonzalezrx8/colorizer");
            //// Create your data collection if it does not exist
            //if (!colorizer.exists())
            //{
            //    colorizer.create();
            //}

        }

        private void OpenFileBtn(object sender, RoutedEventArgs e)
        {
            string fileTypes = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = fileTypes;
            openDlg.Title = "Browse desired Image";
            openDlg.ShowDialog();
            if (openDlg.FileName != "")
            {
                SelectedFile.Text = openDlg.FileName;
            } else
            {
                SelectedFile.Text = "No file selected";
            }

        }
    }
}
