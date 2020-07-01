using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using Microsoft.Win32;
using Model;

namespace ViewModel
{
    public interface IUIServises
    {
        bool CanDraw();
        List<object> SelectedItems();

        string SavePath();

        string OpenPath();

        bool CanAdd();

        bool WantToSave();

    }
    public class ViewM
    {

        public ViewM(IUIServises uiServices)
        {
            this.uiServices = uiServices;

            drawCommand = new RelayCommand(_ => uiServices.CanDraw(), _ => ModelView.Draw(chart, uiServices.SelectedItems()));

            deleteCommand = new RelayCommand(_ => { foreach (ModelData model in uiServices.SelectedItems())
                                                        if (!ModelDataOb.Contains(model)) return false; return true; },
                                             _ => { foreach (ModelData model in uiServices.SelectedItems())
                                                        ModelDataOb.Remove(model); }
                                             );

            saveCommand = new RelayCommand(_ => ModelDataOb.Change,
                                           _ => { 
                                                    string path = uiServices.SavePath();
                                                    if (path != null)
                                                    {
                                                       ModelDataOb.Change = false;
                                                       ObservableModelData.Save(new SerializeAdapter(File.Create(path)), ModelDataOb); 
                                                    } 
                                                 }
                                           );

            addCommand = new RelayCommand(_ => uiServices.CanAdd(),
                                          _ => ModelDataOb.Add(new ModelData(model.N, model.P))
                                          );

            newCommand = new RelayCommand(_ => true,
                                          _ => { 
                                                   if (ModelDataOb.Change && uiServices.WantToSave())
                                                        saveCommand.Execute(null); 
                                                   ModelDataOb.Clear(); 
                                                   ModelDataOb.Change = false; 
                                               }
                                          );

            openCommand = new RelayCommand(_ => true,
                                           _ => { 
                                                    if (ModelDataOb.Change && uiServices.WantToSave())
                                                        { saveCommand.Execute(null); } 
                                                    string path = uiServices.OpenPath();
                                                    if (path != null) 
                                                    { 
                                                       ObservableModelData sample= new ObservableModelData();
                                                       ObservableModelData.Load(new DeserializeAdapter(File.OpenRead(path)), ref sample);
                                                       ModelDataOb.copy(sample);
                                                    } 
                                                }
                                           );
        }

        public static Chart chart = new Chart();

        private IUIServises uiServices;

        public static ObservableModelData ModelDataOb = new ObservableModelData();

        public ObservableModelData MDO
        {
            get { return ModelDataOb; }
        }
   
        public static ModelDataView ModelView = new ModelDataView();

        public ModelDataView MV
        {
            get { return ModelView; }
        }
        public static ModelData model = new ModelData(10,10);

        public ModelData M
        {
            get { return model; }
        }

        private readonly ICommand drawCommand;
        public ICommand DrawCommand { get { return drawCommand; } }

        private readonly ICommand deleteCommand;
        public ICommand DeleteCommand { get { return deleteCommand; } }

        private readonly ICommand saveCommand;
        public ICommand SaveCommand { get { return saveCommand; } }

        private readonly ICommand addCommand;
        public ICommand AddCommand { get { return addCommand; } }

        private readonly ICommand newCommand;
        public ICommand NewCommand { get { return newCommand; } }

        private readonly ICommand openCommand;
        public ICommand OpenCommand { get { return openCommand; } }

        public void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ModelDataOb.Change && uiServices.WantToSave())
                saveCommand.Execute(null);
        }

    }
}
