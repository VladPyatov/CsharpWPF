using Lab4Lib;
using System;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TeamObservable TeamOb = new TeamObservable("MSU","MathAn", "LinAl", "FA", "Physics");
        public Researcher researcher = new Researcher();
        Window1 ResWindow = new Window1();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Pyatov Vladislav";
            this.Closing += MainWindow_Closing;
            this.Topmost=true;
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
            this.DataContext = TeamOb;
            TeamOb.CollectionChanged += CollectionChangedEventHandler;

            LeftListBox.ItemsSource = TeamOb;

            ListCollectionView CollectionView = new ListCollectionView(TeamOb);
            CollectionView.Filter = TeamOb.FilterByResearcher;
            RightListBox.ItemsSource = CollectionView;

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TeamOb.Change)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                    Save(TeamOb);
            }
        }
        //private void WindowBind()
        //{
        //    Binding bd = new Binding();
        //    bd.Source = researcher;
        //    bd.Path = new PropertyPath("Name[0]");
        //    bd.Mode = BindingMode.OneWayToSource;
        //    ResWindow.TextBoxName.SetBinding(TextBox.TextProperty, bd);

        //    Binding bd1 = new Binding();
        //    bd1.Source = researcher;
        //    bd1.Path = new PropertyPath("Name[1]");
        //    bd1.Mode = BindingMode.OneWayToSource;
        //    ResWindow.TextBoxLastName.SetBinding(TextBox.TextProperty, bd1);

        //    Binding bd2 = new Binding();
        //    bd2.Source = researcher;
        //    bd2.Path = new PropertyPath("Date");
        //    bd2.Mode = BindingMode.OneWayToSource;
        //    ResWindow.DatePick.SetBinding(DatePicker.TextProperty, bd2);

        //    Binding bd3 = new Binding();
        //    bd3.Source = researcher;
        //    bd3.Path = new PropertyPath("Topic");
        //    bd3.Mode = BindingMode.OneWayToSource;
        //    ResWindow.CBox.SetBinding(ComboBox.SelectedValueProperty, bd3);

        //    Binding bd4 = new Binding();
        //    bd4.Source = researcher;
        //    bd4.Path = new PropertyPath("num");
        //    bd4.Mode = BindingMode.OneWayToSource;
        //    ResWindow.Num.SetBinding(TextBox.TextProperty, bd4);

        //    ResWindow.AddButton.Click += AddButton_Click;
        //}

        //private void Refresh(TeamObservable t)
        //{
        //    LeftListBox.ItemsSource = t;
        //    RightListBox.ItemsSource = from item in t where item is Researcher select item;
        //    //RightListBox.ItemTemplate = 
        //}

        private void Save(TeamObservable t)
        {
            
            SaveFileDialog save_dialog = new SaveFileDialog();
            if (save_dialog.ShowDialog() == true)
            {
                t.Change = false;
                TeamObservable.Save(save_dialog.FileName, t);
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamOb.Change)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (result == MessageBoxResult.Yes)
                    Save(TeamOb);
            }
            TeamOb.Clear();
            TeamOb.Name = "MSU";

            TeamOb.Change = false;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {

            if (TeamOb.Change)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                    Save(TeamOb);
            }

            OpenFileDialog open_dialog = new OpenFileDialog();
            if (open_dialog.ShowDialog() == true)
            {
                TeamObservable.Load(open_dialog.FileName, ref TeamOb);

                TeamOb.CollectionChanged += CollectionChangedEventHandler;
                this.DataContext = TeamOb;

                LeftListBox.ItemsSource = TeamOb;

                ListCollectionView CollectionView = new ListCollectionView(TeamOb);
                CollectionView.Filter = TeamOb.FilterByResearcher;
                RightListBox.ItemsSource = CollectionView;
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save(TeamOb);
        }

        private void AddDefRes_Click(object sender, RoutedEventArgs e)
        {
            TeamOb.AddDefaultResearcher();
        }

        private void AddCusRes_Click(object sender, RoutedEventArgs e)
        {
            ResWindow = new Window1
            {
                Owner = this,
                DataContext = researcher
            };
            ResWindow.CBox.ItemsSource = TeamOb.Topics;

            if(ResWindow.ShowDialog()==true)
            {
                TeamOb.AddPerson((Researcher)researcher.DeepCopy());
            }
        }
        private void AddDefProg_Click(object sender, RoutedEventArgs e)
        {
            TeamOb.AddDefaultProgrammer();
        }

        private void AddDef_Click(object sender, RoutedEventArgs e)
        {
            TeamOb.AddDefaults();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (RightListBox.IsKeyboardFocusWithin)
                TeamOb.Remove((Person)RightListBox.SelectedItem);
            else if (LeftListBox.IsKeyboardFocusWithin)
                TeamOb.RemoveAt(LeftListBox.SelectedIndex);
            else
                MessageBox.Show("Ничего не выбрано", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((TeamObservable)sender).Change = true;
            ((TeamObservable)sender).Percent = TeamOb.Count() == 0 ? 0 : (double) TeamOb.Count(t => t is Researcher) / TeamOb.Count();

        }

        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TBox.Text != null)
            {
                TeamOb.Name = TBox.Text;
            }
        }

        private void WithoutRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LeftListBox.ItemTemplate = null;
        }

        private void WithRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LeftListBox.ItemTemplate = this.FindResource("listTemplate") as DataTemplate;
        }

        private void RightListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((Researcher)RightListBox.SelectedItem != null)
            {
                BirthTBlock.Text = ((Researcher)RightListBox.SelectedItem).Date.ToShortDateString();
                TopicTBlock.Text = ((Researcher)RightListBox.SelectedItem).Topic.ToString();
                NumOfPubTBlock.Text = ((Researcher)RightListBox.SelectedItem).num.ToString();
            }
        }

        private void LeftListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RightListBox.SelectedIndex = -1;
        }
    }
}
