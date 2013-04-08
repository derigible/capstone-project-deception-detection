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

        public PartWindow(Experiment ex)
        {
            InitializeComponent();
            lblExperiment.Content = ex.name;

            foreach (Participant p in db.Participants)
            {
                this.lstParticipants.Items.Add(p);
            }
            foreach (Condition c in db.Conditions)
            {
                this.conditionName.Items.Add(c);
            }
            this.participantsCount = db.Participants.Count<Participant>();
        }

        private void lstParticipants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.displayPInfo((Participant) lstParticipants.SelectedItem);

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
            this.addPInfo(p);
            db.Participants.Add(p);
            this.lstParticipants.Items.Add(p);
            db.SaveChanges();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.SaveChanges();
        }

        private void displayPInfo(Participant p)
        {
            txtFirstName.Text = p.first_name;
            this.txtLastName.Text = p.last_name;
            this.txtCode.Text = p.code;
            this.txtCity.Text = p.city;
            this.txtEmail.Text = p.email;
            this.txtPhone.Text = p.phone_num;
            this.txtState.Text = p.state;
            this.txtZip.Text = p.zip;
            this.txtAddress1.Text = p.address_line_1;
            this.txtAddress2.Text = p.address_line_2;
            Condition c = (Condition)this.conditionName.SelectedItem;
            List<Participant_condition> pc = p.Participant_condition.Where(r =>
                r.participant_id == p.participant_id).ToList<Participant_condition>();

        }

        private void addPInfo(Participant p)
        {
            p.first_name = txtFirstName.Text;
            p.last_name = this.txtLastName.Text;
            p.code = this.txtCode.Text;
            p.city = this.txtCity.Text;
            p.email = this.txtEmail.Text;
            p.phone_num = this.txtPhone.Text;
            p.state = this.txtState.Text;
            p.zip = this.txtZip.Text;
            p.address_line_1 = this.txtAddress1.Text;
            p.address_line_2 = this.txtAddress2.Text;
            if (!this.conditionName.Items.IsEmpty)
            {
                Condition c = (Condition)this.conditionName.SelectedItem;
                db.Participant_condition.Add(new Participant_condition(db.Participant_condition.Count<Participant_condition>() +1,
                    p.participant_id,
                    c.condition_id));
            }
            Console.WriteLine(db.Participant_condition.Count<Participant_condition>());
        }
    }
}
