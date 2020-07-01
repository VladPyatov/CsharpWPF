using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model 
{
    interface IDeepCopy { object DeepCopy(); }
    [Serializable]
    public class ModelData : IDataErrorInfo, IDeepCopy, INotifyPropertyChanged
    {
        private int n { get; set; }
        private double p { get; set; }
        public double[] grid_points { get; set; }
        public double[] grid_func { get; set; }

        // Area of allowable p-values
        static double pMin = double.NegativeInfinity;
        static double pMax = double.PositiveInfinity;

        // Number of grid points
        static int nMin = 2;
        static int nMax = 1000;

        public event PropertyChangedEventHandler PropertyChanged;

        public ModelData(int n, double p)
        {
            this.n = n;
            this.p = p;
            this.grid_points = new double[n];
            this.grid_func = new double[n];

            for (int i = 0; i < n; i++)
            {
                grid_points[i] = (double)i / (n - 1);
                grid_func[i] = Math.Sin(p * i / (n - 1));
            }


        }

        public int N
        {
            get
            {
                return n;
            }
            set
            {
                n = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("N"));
            }
        }

        public double P
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("P"));
            }
        }

        public int NMin => nMin;
        public int NMax => nMax;

        public double PMin => pMin;
        public double PMax => pMax;


        public double F(double x)
        {
            int i = 0;
            while (x > grid_points[i]) i++;

            if (x == grid_points[i]) return grid_func[i];
            else
            {
                double a = (grid_func[i] - grid_func[i - 1]) / (grid_points[i] - grid_points[i - 1]);
                double b = (grid_func[i] + grid_func[i - 1] - a * (grid_points[i] + grid_points[i - 1])) / 2;
                return a * x + b;
            }
        }

        public string Error { get { return "Error Text"; } }
        public string this[string property]
        {
            get
            {
                string message = null;
                switch(property)
                {
                    case "N":
                        if (n == 1)
                            message = "Grid can not consist of a single point";
                        else if (n < nMin || n > nMax)
                            message = "n must be less " + nMax + " and greater " + nMin;
                        break;
                    case "P":
                        if (p < pMin || p > pMax)
                            message = "p must be less " + pMax + " and greater " + pMin;
                        break;
                    default:
                        break;
                }
                return message;
            }
        }

        public virtual object DeepCopy()
        {
            return new ModelData(this.n, this.p);
        }

        public override string ToString()
        {
            return "Number of points = " + n + "; Function parameter = " + p + ".";
        }
    }
}
