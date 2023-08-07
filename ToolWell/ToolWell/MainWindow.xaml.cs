using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
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
using ToolWell.Services;
using ToolWell.ViewModel;
using ToolWell.Interfaces;
//This application is using images from Flaticon.com
namespace ToolWell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            var serviceClient= new OilWellToolServiceClient("https://localhost:7073/api/OilWellTool", new MessageBoxService());
            var viewModel = new OilWellToolViewModel(serviceClient,new MessageBoxService());
            DataContext = viewModel;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                //This will defer the SelectAll action until all other events(like the mouse click) have been processed so that it can select all the text.
                textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()), System.Windows.Threading.DispatcherPriority.Background);
            }
        }
    }
}
