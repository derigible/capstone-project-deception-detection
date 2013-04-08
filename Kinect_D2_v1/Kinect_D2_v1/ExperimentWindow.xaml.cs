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
using System.Windows.Shapes;

namespace Kinect_D2_v1
{
    /// <summary>
    /// Interaction logic for Experiment.xaml
    /// </summary>
    public partial class ExperimentWindow : Window
    {
        public ExperimentWindow()
        {
            InitializeComponent();

            lbExperimentList.Items.Add("Single Decimal Theory");
            lbExperimentList.Items.Add("Meh");
        }

        private void click_btnOpenExperiment(object sender, RoutedEventArgs e)
        {
            PartWindow participant = new PartWindow(tbExperimentName.Text.ToString());
            participant.Show();
            Close();
        }

        private void lbExperimentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbExperimentName.Text = lbExperimentList.SelectedValue.ToString();
            btnOpenExperiment.IsEnabled = true;
            btnExport.IsEnabled = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
