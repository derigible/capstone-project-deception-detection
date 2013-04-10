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
    /// Interaction logic for TagsWindow.xaml
    /// </summary>
    public partial class TagsWindow : Window
    {
        DeceptionDBEntities db = new DeceptionDBEntities();
        public TagsWindow()
        {
            InitializeComponent();

            foreach (Tag tag in db.Tags)
            {
                this.lstTags.Items.Add(tag);
            }
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            if (txtTag.Text != "")
            {
                this.warning.Content = "";
                Tag tag = new Tag(this.txtTag.Text);
                lstTags.Items.Add(tag);
                db.Tags.Add(tag);
                db.SaveChanges();
            }
            else
            {
                this.warning.Content = "You must add a tag name.";
            }
        }

        private void lstTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tag tag = (Tag) this.lstTags.SelectedItem;
            if (tag != null)
            {
                this.txtTag.Text = tag.description;
            }
            else
            {
                this.txtTag.Text = "";
            }
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = (Tag)this.lstTags.SelectedItem;
            if (tag != null)
            {
                this.warning.Content = "";
                this.lstTags.Items.Remove(tag);
                db.Tags.Remove(tag);
            }
            else
            {
                this.warning.Content = "You must select a tag to remove.";
            }
        }
    }
}
