using Microsoft.Kinect;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        private DeceptionDBEntities db = new DeceptionDBEntities();
        private String[] headers = { "raw_data_id", "timestamp", "Head_X", "Head_Y", "Head_Z", 
                                       "ShoulderCenter_X", "ShoulderCenter_Y", "ShoulderCenter_Z", 
                                       "ShoulderLeft_X", "ShoulderLeft_Y", "ShoulderLeft_Z", "ShoulderRight_X", 
                                       "ShoulderRight_Y", "ShoulderRight_Z", "Spine_X", "Spine_Y", "Spine_Z",
                                       "HipCenter_X", "HipCenter_Y", "HipCenter_Z", "HipLeft_X", "HipLeft_Y", 
                                       "HipLeft_Z", "HipRight_X", "HipRight_Y", "HipRight_Z", "ElbowLeft_X", 
                                       "ElbowLeft_Y", "ElbowLeft_Z", "WristLeft_X", "WristLeft_Y", "WristLeft_Z", 
                                       "HandLeft_X", "HandLeft_Y", "HandLeft_Z", "ElbowRight_X", "ElbowRight_Y", 
                                       "ElbowRight_Z", "WristRight_X", "WristRight_Y", "WristRight_Z", "HandRight_X", 
                                       "HandRight_Y", "HandRight_Z", "KneeLeft_X", "KneeLeft_Y", "KneeLeft_Z", 
                                       "AnkleLeft_X", "AnkleLeft_Y", "AnkleLeft_Z", "FootLeft_X", "FootLeft_Y", 
                                       "FootLeft_Z", "KneeRight_X", "KneeRight_Y", "KneeRight_Z", "AnkleRight_X",
                                       "AnkleRight_Y", "AnkleRight_Z", "FootRight_X", "FootRight_Y", "FootRight_Z"};

        private Experiment ex;
        public ExportWindow(Experiment ex)
        {
            this.ex = ex;
            InitializeComponent();
            this.participants_list.SelectedIndex = 0;

            // This does not return the list of participants in the experiment, but rather it just
            // returns all participants in the database. future work should be to determine only those
            //participants in the experiment show up
            foreach (Participant p in db.Participants)
            {
                this.participants_list.Items.Add(p);
            }

            for (int i = 0; i < headers.Length; i++)
            {
                this.data_selection_list.Items.Add(headers[i]);
            }
        }

        public void keepSelected(Participant p, ExcelWorksheet worksheet)
        {
            var list = this.data_selection_list.SelectedItems;
            long tempID = p.Participant_Condition.First().pc_id;
            MessageBox.Show("PC ID is "+tempID);
            List<Raw_Data> data_points = db.Raw_Data
                .Where<Raw_Data>(r => r.pc_id == tempID).ToList();
            MessageBox.Show("data points size is is " + data_points.Count);
            int i = 1;
            foreach (String s in list)
            {
                Console.WriteLine(s);
            }
            foreach( Raw_Data data in data_points)
            {
                int j = 1;

                if (list.Contains("raw_data_id"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "raw_data_id";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.raw_data_id;
                        j++;
                    }
                }
                if (list.Contains("timestamp"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "timestamp";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.timestamp;
                        j++;
                    }
                }
                if (list.Contains("Head_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "Head_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.Head_X;
                        j++;
                    }
                }
                if (list.Contains("Head_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "Head_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.Head_Y;
                        j++;
                    }
                }
                if (list.Contains("Head_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "Head_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.Head_Z;
                        j++;
                    }
                }
                if (list.Contains("ShoulderCenter_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderCenter_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderCenter_X;
                        j++;
                    }
                }
                if (list.Contains("ShoulderCenter_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderCenter_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderCenter_Y;
                        j++;
                    }
                }
                if (list.Contains("ShoulderCenter_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderCenter_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderCenter_Z;
                        j++;
                    }
                }
                if (list.Contains("ShoulderLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderLeft_X;
                        j++;
                    }
                }
                if (list.Contains("ShoulderLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("ShoulderLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("ShoulderRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderRight_X;
                        j++;
                    }
                }
                if (list.Contains("ShoulderRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderRight_Y;
                        j++;
                    }
                }
                if (list.Contains("ShoulderRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ShoulderRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ShoulderRight_Z;
                        j++;
                    }
                }
                if (list.Contains("Spine_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "Spine_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.Spine_X;
                        j++;
                    }
                }
                if (list.Contains("Spine_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "Spine_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.Spine_Y;
                        j++;
                    }
                }
                if (list.Contains("Spine_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "Spine_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.Spine_Z;
                        j++;
                    }
                }
                if (list.Contains("HipCenter_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipCenter_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipCenter_X;
                        j++;
                    }
                }
                if (list.Contains("HipCenter_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipCenter_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipCenter_Y;
                        j++;
                    }
                }
                if (list.Contains("HipCenter_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipCenter_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipCenter_Z;
                        j++;
                    }
                }
                if (list.Contains("HipLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipLeft_X;
                        j++;
                    }
                }
                if (list.Contains("HipLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("HipLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("HipRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipRight_X;
                        j++;
                    }
                }
                if (list.Contains("HipRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipRight_Y;
                        j++;
                    }
                }
                if (list.Contains("HipRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HipRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HipRight_Z;
                        j++;
                    }
                }
                if (list.Contains("ElbowLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ElbowLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ElbowLeft_X;
                        j++;
                    }
                }
                if (list.Contains("ElbowLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ElbowLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ElbowLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("ElbowLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ElbowLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ElbowLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("WristLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "WristLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.WristLeft_X;
                        j++;
                    }
                }
                if (list.Contains("WristLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "WristLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.WristLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("WristLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "WristLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.WristLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("HandLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HandLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HandLeft_X;
                        j++;
                    }
                }
                if (list.Contains("HandLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HandLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HandLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("HandLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HandLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HandLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("ElbowRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ElbowRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ElbowRight_X;
                        j++;
                    }
                }
                if (list.Contains("ElbowRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ElbowRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ElbowRight_Y;
                        j++;
                    }
                }
                if (list.Contains("ElbowRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "ElbowRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.ElbowRight_Z;
                        j++;
                    }
                }
                if (list.Contains("WristRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "WristRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.WristRight_X;
                        j++;
                    }
                }
                if (list.Contains("WristRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "WristRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.WristRight_Y;
                        j++;
                    }
                }
                if (list.Contains("WristRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "WristRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.WristRight_Z;
                        j++;
                    }
                }
                if (list.Contains("HandRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HandRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HandRight_X;
                        j++;
                    }
                }
                if (list.Contains("HandRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HandRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HandRight_Y;
                        j++;
                    }
                }
                if (list.Contains("HandRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "HandRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.HandRight_Z;
                        j++;
                    }
                }
                if (list.Contains("KneeLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "KneeLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.KneeLeft_X;
                        j++;
                    }
                }
                if (list.Contains("KneeLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "KneeLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.KneeLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("KneeLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "KneeLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.KneeLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("AnkleLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "AnkleLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.AnkleLeft_X;
                        j++;
                    }
                }
                if (list.Contains("AnkleLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "AnkleLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.AnkleLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("AnkleLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "AnkleLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.AnkleLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("FootLeft_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "FootLeft_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.FootLeft_X;
                        j++;
                    }
                }
                if (list.Contains("FootLeft_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "FootLeft_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.FootLeft_Y;
                        j++;
                    }
                }
                if (list.Contains("FootLeft_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "FootLeft_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.FootLeft_Z;
                        j++;
                    }
                }
                if (list.Contains("KneeRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "KneeRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.KneeRight_X;
                        j++;
                    }
                }
                if (list.Contains("KneeRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "KneeRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.KneeRight_Y;
                        j++;
                    }
                }
                if (list.Contains("KneeRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "KneeRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.KneeRight_Z;
                        j++;
                    }
                }
                if (list.Contains("AnkleRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "AnkleRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.AnkleRight_X;
                        j++;
                    }
                }
                if (list.Contains("AnkleRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "AnkleRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.AnkleRight_Y;
                        j++;
                    }
                }
                if (list.Contains("AnkleRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "AnkleRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.AnkleRight_Z;
                        j++;
                    }
                }
                if (list.Contains("FootRight_X"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "FootRight_X";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.FootRight_X;
                        j++;
                    }
                }
                if (list.Contains("FootRight_Y"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "FootRight_Y";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.FootRight_Y;
                        j++;
                    }
                }
                if (list.Contains("FootRight_Z"))
                {
                    if (i == 1)
                    {
                        worksheet.Cells[i, j].Value = "FootRight_Z";
                        j++;
                    }
                    else
                    {
                        worksheet.Cells[i, j].Value = data.FootRight_Z;
                        j++;
                    }
                } 
                i++;
            }
        }

        private void export(object sender, RoutedEventArgs e)
        {
            var temp = this.participants_list.SelectedItems;
            if (temp != null)
            {
                if (this.data_selection_list.SelectedItems.Count != 0)
                {
                    // Configure save file dialog box
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "Raw_data_" + ex.name; // Default file name
                    dlg.DefaultExt = ".xlsx"; // Default file extension
                    dlg.Filter = "Excel Worksheets (*.xlsx)|*.xlsx"; // Filter files by extension
                    // Show save file dialog box
                    Nullable<bool> result = dlg.ShowDialog();

                    // Process save file dialog box results
                    string filename = "";

                    if (result == true)
                    {
                        // Save document
                        filename = dlg.FileName;
                        FileInfo newFile = new FileInfo(filename);
                        if (newFile.Exists)
                        {
                            newFile.Delete();  // ensures we create a new workbook
                            newFile = new FileInfo(filename);
                        }

                        ExcelPackage package = new ExcelPackage(newFile);

                        foreach (Participant p in temp)
                        {
                            Console.WriteLine(p);
                            this.keepSelected(p, package.Workbook.Worksheets.Add(p.lname + "_" + p.fname));
                        }

                        // set some document properties
                        package.Workbook.Properties.Title = "Raw_Data";
                        package.Workbook.Properties.Author = "Experimenter";
                        package.Workbook.Properties.Comments = "Raw_Data";

                        // set some extended property values
                        package.Workbook.Properties.Company = "BYU";

                        // save our new workbook and we are done!
                        package.Save();
                    }
                }
                else
                {
                    MessageBox.Show("You must select at least one data point.");
                }
            }
            else
            {
                MessageBox.Show("You must select a participant.");
            }
        }
    }
}
