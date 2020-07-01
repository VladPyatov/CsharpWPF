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
using ViewModel;
using Control = System.Windows.Controls.Control;
using ListBox = System.Windows.Controls.ListBox;
using Grid = System.Windows.Controls.Grid;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Title = "Pyatov Vladislav, 301 group.";
            ViewM M = new ViewM(new MyAppUIServices(ref ListBox1, ref small_grid, ref add_grid));
            DataContext = M;

            Closing += M.MainWindow_Closing;
            winFormsHost.Child = ViewM.chart;
        }

    }

    public class MyAppUIServices : IUIServises
    {
        private ListBox ListBox1;
        private Grid small_grid;
        private Grid add_grid;
        public MyAppUIServices(ref ListBox listbox, ref Grid small_grid, ref Grid add_grid)
        {
            this.ListBox1 = listbox;
            this.small_grid = small_grid;
            this.add_grid = add_grid;
        }
        public bool CanDraw()
        {
            bool flag = ListBox1 != null && ListBox1.SelectedItems.Count > 0;

            foreach (FrameworkElement child in small_grid.Children)
                if (Validation.GetHasError(child) == true)
                {
                    flag = false;
                }
            return flag;
        }

        public List<object> SelectedItems()
        {
            List<object> ModelList = new List<object>();
            foreach (var item in ListBox1.SelectedItems)
                ModelList.Add(item);

            return ModelList;
        }

        public string SavePath()
        {

            SaveFileDialog save_dialog = new SaveFileDialog();
            if (save_dialog.ShowDialog() == true)
                return save_dialog.FileName;
            else
                return null;
        }

        public bool CanAdd()
        {
            bool flag = true;

            foreach (FrameworkElement child in add_grid.Children)
                if (Validation.GetHasError(child) == true)
                    flag = false;

            return flag;
        }
        public bool WantToSave()
        {
            return MessageBox.Show("Сохранить изменения?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes;
        }

        public string OpenPath()
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            if (open_dialog.ShowDialog() == true)
                return open_dialog.FileName;
            else
                return null;
        }

    }
}
