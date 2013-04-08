using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Kinect_D2_v1
{
    /// <summary>
    /// Interaction logic for LaunchWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {

        private void open_export_window_Click(object sender, RoutedEventArgs e)
        {
            ExportWindow export = new ExportWindow();
            export.Show();
        }

        private void open_experiments_window_Click(object sender, RoutedEventArgs e)
        {
            ExperimentWindow exp = new ExperimentWindow();
            exp.Show();
        }
    }
}
