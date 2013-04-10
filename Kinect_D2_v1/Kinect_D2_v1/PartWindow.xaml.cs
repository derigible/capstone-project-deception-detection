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
        DeceptionDBEntities db = new DeceptionDBEntities();

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
        }

        private void lstParticipants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.displayPInfo((Participant) lstParticipants.SelectedItem);

            btnCapture.IsEnabled = true;
            update.IsEnabled = true;
        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            Participant p = (Participant) this.lstParticipants.SelectedItem;
            MainWindow captureScreen = new MainWindow(p.Participant_Condition.First(), this);
            captureScreen.Show();
            Hide();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ExperimentWindow experiment = new ExperimentWindow();
            experiment.Show();
            Close();
        }

        private void btnAddParticipant_Click(object sender, RoutedEventArgs e)
        {
            Participant p = new Participant(this.txtCode.Text,
                this.txtFirstName.Text, this.txtLastName.Text);
            db.Participants.Add(p);
            this.addPInfo(p);
            this.lstParticipants.Items.Add(p);
            db.SaveChanges();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.SaveChanges();
        }

        private void displayPInfo(Participant p)
        {
            txtFirstName.Text = p.fname;
            this.txtLastName.Text = p.lname;
            this.txtCode.Text = p.code;
            this.txtCity.Text = p.city;
            this.txtEmail.Text = p.email;
            this.txtPhone.Text = p.pnum;
            this.txtState.Text = p.state;
            this.txtZip.Text = p.zip;
            this.txtAddress1.Text = p.add_line_1;
            this.txtAddress2.Text = p.add_line_2;
            List<Participant_Condition> pc = p.Participant_Condition.Where(r =>
                r.participant_id == p.participant_id).ToList<Participant_Condition>();
            Participant_Condition pc1 = pc.First<Participant_Condition>();
            Condition con = pc1.Condition;
            if (con != null)
            {
                this.conditionName.SelectedItem = con;
            }
            else
            {
                Console.WriteLine("The con was null");
            }
        }

        private void addPInfo(Participant p)
        {
            p.fname = txtFirstName.Text;
            p.lname = this.txtLastName.Text;
            p.code = this.txtCode.Text;
            p.city = this.txtCity.Text;
            p.email = this.txtEmail.Text;
            p.pnum = this.txtPhone.Text;
            p.state = this.txtState.Text;
            p.zip = this.txtZip.Text;
            p.add_line_1 = this.txtAddress1.Text;
            p.add_line_2 = this.txtAddress2.Text;
            if (!this.conditionName.Items.IsEmpty)
            {
                Condition c = (Condition)this.conditionName.SelectedItem;
                if (c != null)
                {
                    db.Participant_Condition.Add(new Participant_Condition(p.participant_id,
                        c.condition_id));
                }
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            this.addPInfo((Participant)this.lstParticipants.SelectedItem);
        }
    }
}
