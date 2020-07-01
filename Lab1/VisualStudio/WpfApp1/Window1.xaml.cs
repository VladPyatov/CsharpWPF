using Lab4Lib;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text == "")
                MessageBox.Show("Enter the Name, please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (TextBoxLastName.Text == "")
                MessageBox.Show("Enter the Last Name, please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (DatePick.Text== "")
                MessageBox.Show("Select the Birth Date, please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (CBox.Text == "")
                MessageBox.Show("Select the Topic, please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (Num.Text == "")
                MessageBox.Show("Enter the Number Of Publications, please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!Int32.TryParse(Num.Text,out _))
                MessageBox.Show("Wrong Number Of Publications value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                this.DialogResult = true;
        }
    }
}