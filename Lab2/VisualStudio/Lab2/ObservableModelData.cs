using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    [Serializable]
    class ObservableModelData: ObservableCollection<ModelData>
    {
        private bool change;
        public bool Change
        {
            get
            {
                return change;
            }
            set
            {
                change = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Change"));
            }
        }

        public void Add_ModelData()
        {
            for (int i = 1; i < 4; i++)
                base.Add(new ModelData(i * 5, i*3));
        }

        void Remove_At(int index)
        {
            base.RemoveAt(index);
        }

        public double[] Calculate(double x)
        {
            double[] func_values = new double[base.Count];

            foreach (ModelData model in base.Items)
                func_values.Append(model.F(x));

            return func_values;
        }
        public override string ToString()
        {
            return "Change: "+change+".\n"+base.ToString();
        }

        public static bool Save(string filename, ObservableModelData obj)
        {
            bool b = true;
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.Serialize(fileStream, obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                b = false;
            }
            finally
            { if (fileStream != null) fileStream.Close(); }

            return b;
        }

        public static bool Load(string filename, ref ObservableModelData obj)
        {
            bool b = true;
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                obj = binaryFormatter.Deserialize(fileStream) as ObservableModelData;
            }
            catch (FileNotFoundException ex)
            {
                //obj.Log = new List<string>();
                b = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                b = false;
            }
            finally
            { if (fileStream != null) fileStream.Close(); }

            return b;
        }

    }
}
