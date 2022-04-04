using CV19.Models.Decanat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CV19.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GroupCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group)) return;
            if (group.Name is null) return;

            var filter_text = groupNameFilterText.Text;
            if(filter_text.Length == 0) return;

            if (group.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (group.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        private void OnGroupsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var text_box = (TextBox)sender;
            var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}
