using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    interface IDeepCopy { object DeepCopy(); }
    [Serializable]
    public class ModelData: IDataErrorInfo, IDeepCopy
    {
        public int n { get; set; }
        public double p { get; set; }
        public double[] grid_points { get; set; }
        public double[] grid_func { get; set; }

        // Area of allowable p-values
        static double pMin=double.NegativeInfinity;
        static double pMax=double.PositiveInfinity;

        // Number of grid points
        static int nMin=2;
        static int nMax=1000;

        public ModelData(int n, double p)
        {
            this.n = n;
            this.p = p;
            this.grid_points = new double[n];
            this.grid_func = new double[n];

            for (int i = 0; i < n; i++)
            {
                grid_points[i] = (double) i / (n - 1);
                grid_func[i] = Math.Sin(p * i / (n - 1));
            }

            
        }

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
                    case "n":
                        if (n < nMin || n > nMax)
                            message = "n must be less " + nMax + " and greater " + nMin;
                        else if (n == 1)
                            message = "Grid can not consist of a single point";
                        break;
                    case "p":
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
    }
}
