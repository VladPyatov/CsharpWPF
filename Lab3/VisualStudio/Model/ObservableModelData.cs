using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class ObservableModelData: ObservableCollection<ModelData>
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

        public ObservableModelData()
        {
            this.Add_ModelData();
            this.CollectionChanged += CollectionChangedEventHandler;
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

        public static bool Save(ISerialize serializer, ObservableModelData obj)
        {
            return serializer.Serialize(obj);
        }

        public static bool Load(IDeserialize deserializer, ref ObservableModelData obj)
        {
            return deserializer.Deserialize(ref obj);
        }

        public void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((ObservableModelData)sender).Change = true;
        }

        public void copy(ObservableModelData ObModelData)
        {
            base.Clear();
            foreach (ModelData MD in ObModelData)
                base.Add((ModelData) MD.DeepCopy());
            this.Change = ObModelData.Change;

        }
    }
    
}
