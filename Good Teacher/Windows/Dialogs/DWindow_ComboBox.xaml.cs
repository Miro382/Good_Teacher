using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_ComboBox.xaml
    /// </summary>
    public partial class DWindow_ComboBox : Window
    {
        DataStore data;
        public bool isOK = false;
        public List<ContentCreator> contents = new List<ContentCreator>();

        public DWindow_ComboBox(DataStore datastore, List<ContentCreator> list)
        {
            InitializeComponent();

            data = datastore;


            foreach (ContentCreator cont in list)
            {
                ContentViewer contV = new ContentViewer();

                contV.contentCreator = cont;
                contV.Content = "";
                contV.Content = cont.Create(data);
                contV.MaxHeight = 80;
                CB_Listbox.Items.Add(contV);
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            ContentCreator contentCreator = new ContentCreator();
            DWindow_Content contentW = new DWindow_Content(data, contentCreator);
            contentW.ShowDialog();

            if (contentW.IsOK == true)
            {
                ContentViewer cont = new ContentViewer();

                cont.contentCreator = contentW.content;
                cont.Content = "";
                cont.Content = cont.contentCreator.Create(data);
                cont.MaxHeight = 80;
                CB_Listbox.Items.Add(cont);
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Listbox.SelectedIndex >= 0)
            {
                CB_Listbox.Items.RemoveAt(CB_Listbox.SelectedIndex);
            }
        }


        private void Edit()
        {
            if (CB_Listbox.SelectedIndex >= 0)
            {
                ContentViewer cont = ((ContentViewer)CB_Listbox.Items[CB_Listbox.SelectedIndex]);
                DWindow_Content contentW = new DWindow_Content(data, cont.contentCreator);
                contentW.ShowDialog();

                if (contentW.IsOK == true)
                {

                    cont.contentCreator = contentW.content;
                    cont.Content = "";
                    cont.Content = cont.contentCreator.Create(data);
                }

            }
        }


        private void CB_Listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }


        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            foreach (ContentViewer cont in CB_Listbox.Items)
            {
                contents.Add(cont.contentCreator);
            }

            isOK = true;
            Close();
        }

    }
}
