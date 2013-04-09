using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Kinect_D2_v1
{
    /// <summary>
    /// Interaction logic for Experiment.xaml
    /// </summary>
    public partial class ExperimentWindow : Window
    {
        KinectDatabaseEntities1 db = new KinectDatabaseEntities1();
        public ExperimentWindow()
        {
            InitializeComponent();

            foreach (Experiment ex in db.Experiments)
            {
                this.lbExperimentList.Items.Add(ex);
            }
        }

        private void click_btnOpenExperiment(object sender, RoutedEventArgs e)
        {
            PartWindow participant = new PartWindow((Experiment)lbExperimentList.SelectedItem);
            participant.Show();
            Close();
        }

        private void lbExperimentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Experiment ex = (Experiment) lbExperimentList.SelectedItem;
            tbExperimentName.Text = lbExperimentList.SelectedValue.ToString();
            btnOpenExperiment.IsEnabled = true;
            btnExport.IsEnabled = true;
            List<Condition> cons = db.Conditions.Where(c => c.experiment_id == ex.experiment_id).ToList<Condition>();
            lstConditions.Items.Clear();
            foreach (Condition con  in cons)
            {
                lstConditions.Items.Add(con);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            Close();
        }

        private void btnAddCondition_Click(object sender, RoutedEventArgs e)
        {
            Experiment ex = (Experiment) lbExperimentList.SelectedItem;
            if (ex != null)
            {
                this.warning.Content = "";
                Condition con = new Condition(ex.experiment_id);
                con.code = this.conCode.Text;
                con.description = this.conDescription.Text;
                long id = db.Conditions.Count<Condition>() + 1;
                Console.WriteLine("Id is: " +id);
                con.condition_id = id;
                db.Conditions.Add(con);
                this.lstConditions.Items.Add(con);
                db.SaveChanges();
            }
            else
            {
                this.warning.Content = "You must select an experiment";
            }
        }

        private void lstConditions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Condition con = (Condition)lstConditions.SelectedItem;
            if (con != null)
            {
                this.conCode.Text = con.code;
                this.conDescription.Text = con.description;
            }
            else
            {
                this.conCode.Text = "";
                this.conDescription.Text = "";
            }
        }

        private void btnRemoveCondition_Click(object sender, RoutedEventArgs e)
        {
            db.Conditions.Remove((Condition)lstConditions.SelectedItem);
            lstConditions.Items.Remove((Condition)lstConditions.SelectedItem);
            db.SaveChanges();
        }

        private void btnAddExperiment_Click(object sender, RoutedEventArgs e)
        {
            Experiment ex = new Experiment(db.Experiments.Count<Experiment>() + 1, tbExperimentName.Text);
            db.Experiments.Add(ex);
            this.lbExperimentList.Items.Add(ex);
            db.SaveChanges();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportWindow export = new ExportWindow((Experiment)lbExperimentList.SelectedItem);
            export.Show();
        }
    }
}
