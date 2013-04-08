using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for PartWindow.xaml
    /// </summary>
    public partial class PartWindow : Window
    {
        KinectDatabaseEntities1 db = new KinectDatabaseEntities1();
        long participantsCount;

        public PartWindow(string experimentName)
        {
            InitializeComponent();
            lblExperiment.Content = experimentName;

            foreach (Participant p in db.Participants)
            {
                this.lstParticipants.Items.Add(p.ToString());
            }
            this.participantsCount = db.Participants.Count<Participant>();
        }

        private void lstParticipants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtFirstName.Text = lstParticipants.SelectedValue.ToString();
            btnCapture.IsEnabled = true;
        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            MainWindow captureScreen = new MainWindow();
            captureScreen.Show();
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ExperimentWindow experiment = new ExperimentWindow();
            experiment.Show();
            Close();
        }

        private void btnAddParticipant_Click(object sender, RoutedEventArgs e)
        {
            Participant p = new Participant(this.participantsCount++,this.txtCode.Text,
                this.txtFirstName.Text, this.txtLastName.Text);
            db.Participants.Add(p);
            this.lstParticipants.Items.Add(p.ToString());
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
