using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MessageBox = System.Windows.MessageBox;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableModelData ModelDataOb = new ObservableModelData();
        private ModelDataView ModelView = new ModelDataView(new ObservableModelData());
        
        Window1 AddWindow = new Window1();
        public static RoutedCommand DrawCommand = new RoutedCommand("Draw", typeof(Lab2.MainWindow));
        Chart chart = new Chart();
        public MainWindow()
        {
            InitializeComponent();
            ModelDataOb.Add_ModelData();
            ListBox1.ItemsSource = ModelDataOb;
            this.DataContext = ModelView;
            this.Title = "Pyatov Vladislav, 301 group.";
            this.Closing += MainWindow_Closing;
            ModelDataOb.CollectionChanged += CollectionChangedEventHandler;
            CBox.ItemsSource = new string[] { "F1", "F2", "F3", "F4", "F5"};
            winFormsHost.Child = chart;

        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ModelDataOb.Change)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                    Save(ModelDataOb);
            }
        }
        private void AddNewData_Click(object sender, RoutedEventArgs e)
        {
            ModelData model = new ModelData(0, 0);
            AddWindow = new Window1
            {
                Owner = this,
                DataContext = model
            };
            if (AddWindow.ShowDialog() == true)
            {
                ModelDataOb.Add((ModelData)model.DeepCopy());
            }
        }
        private void Save(ObservableModelData t)
        {

            SaveFileDialog save_dialog = new SaveFileDialog();
            if (save_dialog.ShowDialog() == true)
            {
                t.Change = false;
                ObservableModelData.Save(save_dialog.FileName, t);
            }
        }

        private void NewCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (ModelDataOb.Change)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (result == MessageBoxResult.Yes)
                    Save(ModelDataOb);
            }
            ModelDataOb.Clear();
            ModelDataOb.Change = false;
        }

        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (ModelDataOb.Change)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                    Save(ModelDataOb);
            }

            OpenFileDialog open_dialog = new OpenFileDialog();
            if (open_dialog.ShowDialog() == true)
            {
                ObservableModelData.Load(open_dialog.FileName, ref ModelDataOb);

                ModelDataOb.CollectionChanged += CollectionChangedEventHandler;

                ListBox1.ItemsSource = ModelDataOb;

            }
        }
        public void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((ObservableModelData)sender).Change = true;
        }

        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ModelDataOb.Change == true)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Save(ModelDataOb);
        }

        private void CanDeleteCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            bool flag = false;
            foreach (ModelData item in ListBox1.SelectedItems)
                if (ModelDataOb.Contains(item))
                    flag = true;
                else
                    flag = false;
            e.CanExecute = flag;
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            List<ModelData> ModelList = new List<ModelData>();
            foreach (ModelData item in ListBox1.SelectedItems)
                ModelList.Add(item);
            foreach (ModelData item in ModelList)
               ModelDataOb.Remove(item);
        }

        private void CanAddCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CanDrawCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            bool flag = ListBox1 != null && ListBox1.SelectedItems.Count > 0;

            foreach(FrameworkElement child in main_grid.Children)
                if (Validation.GetHasError(child)==true)
                {
                    flag = false;
                }
            if (flag)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void DrawCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            ModelView.Draw(chart, ListBox1.SelectedItems);
        }
    }
}
