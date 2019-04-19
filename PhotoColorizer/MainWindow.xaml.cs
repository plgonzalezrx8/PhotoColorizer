using System;
using System.Collections.Generic;
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



        public static void loadImage()
        {
            //this is a method used to load the local image into memory that will be used for the algorithm


        }

        public static void PicColorize()

        {

            var input = "{"
                + "  \"image\": \"data://deeplearning/example_data/lincoln.jpg\"" // this should be changed to the image being loaded
                + "}";
            var client = new Client("simnLzihd3NUcUIv9sCGXja7NOI1");
            var algorithm = client.algo("deeplearning/ColorfulImageColorization/1.1.13");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(input);
            Console.WriteLine(response.result);


        }
    }
}
