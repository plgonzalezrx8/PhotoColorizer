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
using Newtonsoft.Json;

namespace PhotoColorizer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        static string imageToUploadFilePath = null;
        static string imageToUploadFileName = null;
        static string downloadedImage = null;



        public MainWindow() {

            InitializeComponent();
        }



        public static void LoadImage() {
            //this is a method used to load the local image into memory that will be used for the algorithm


        }

        public static void PicColorize() {
            JObject APIkey = JObject.Parse(File.ReadAllText(@"C:\Users\plgon\source\repos\PhotoColorizer\PhotoColorizer\APIkey.json"));




            var input = "{"
                + "  \"image\": \"data://plgonzalezrx8/PhotoColorizer/" + imageToUploadFileName + "\"" // this should be changed to the image being loaded
                + "}";
            var client = new Client((string)APIkey["APIkey"]); //change the API for it to be loaded from a JSON file not exposed to Github
            var nlp_directory = client.dir("data://plgonzalezrx8/PhotoColorizer");
            var local_file_path = imageToUploadFilePath;
            var destination = nlp_directory.file(imageToUploadFileName);
            var algorithm = client.algo("deeplearning/ColorfulImageColorization/1.1.13");

            if (!destination.exists()) {
                destination.put(File.OpenRead(local_file_path));
            }
            Console.WriteLine("After");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(input);
            dynamic outputFile = JObject.Parse(response.result.ToString());

            dynamic output = outputFile.output;

         

            Console.WriteLine("Test");
            Console.WriteLine(output);

            Console.WriteLine(response.result);

            

            string uploadedFileDir = output;

            if (client.file(uploadedFileDir).exists()) {
                var localFile = client.file(uploadedFileDir).getFile();
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save the colorized image";
                saveFileDialog1.Filter = "PNG Image|*.png";
                saveFileDialog1.ShowDialog();
                byte[] b = null;
                if (saveFileDialog1.FileName != "") {
                    using (FileStream f = localFile) {
                        b = new byte[f.Length];
                        f.Read(b, 0, b.Length);

                    }





                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                    using (StreamWriter sw = new StreamWriter(s)) {
                        s.Write(b, 0, b.Length);

                        
                    }


                        




                }





            } else {
                Console.WriteLine("The file does not exist");
            }





        }

        public static void PicUploader() //uploads the picture to the algoritmia server
        {



        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            //JObject APIkey = JObject.Parse(File.ReadAllText(@"C:\Users\plgon\source\repos\PhotoColorizer\PhotoColorizer\APIkey.json"));
            //var client = new Client((string)APIkey["APIkey"]); //change the API for it to be loaded from a JSON file not exposed to Github
            //var colorizer = client.dir("data://plgonzalezrx8/colorizer");
            //// Create your data collection if it does not exist
            //if (!colorizer.exists())
            //{
            //    colorizer.create();
            //}

        }

        private void OpenFileBtn(object sender, RoutedEventArgs e) {

            string fileTypes = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = fileTypes;
            openDlg.Title = "Browse desired Image";
            openDlg.ShowDialog();

            if (openDlg.FileName != "") {
                SelectedFile.Text = openDlg.FileName;
                imageToUploadFilePath = openDlg.FileName;
                imageToUploadFileName = openDlg.SafeFileName;

                
            } else {
                SelectedFile.Text = "No file selected";
                imageToUploadFilePath = null;
                imageToUploadFileName = null;
            }

        }

        private void Colorize_Click(object sender, RoutedEventArgs e) {

            PicColorize();

        }
    }
}
