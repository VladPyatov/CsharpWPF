using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab2
{
    class ModelDataView : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ModelDataView(ObservableModelData model_collection)
        {

        }

        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
            }
        }

        private string format;
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Format"));
            }
        }

        public void Draw(Chart chart, IList selectedDataModels)
        {
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.BackColor = Color.Lavender;
            chart.ChartAreas.Add(new ChartArea("chartArea1"));
            chart.ChartAreas.Add(new ChartArea("chartArea2"));
            chart.ChartAreas[0].AxisX.LabelStyle.Format = format;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = format;
            chart.ChartAreas[0].AxisX.Title = "x";
            chart.ChartAreas[0].AxisY.Title = "F(x)";
            Legend L = new Legend();
            L.BackColor = Color.Turquoise;
            L.Name = "Lega";
            L.BackGradientStyle = GradientStyle.DiagonalRight;
            ElementPosition p = new ElementPosition(1,1,10,10);
            //L.Position = p;
            chart.Legends.Add(L);



            chart.ChartAreas[1].AxisX.LabelStyle.Format = format;
            chart.ChartAreas[1].AxisX.LabelStyle.Format = format;
            chart.ChartAreas[1].AxisX.Title = "p";
            chart.ChartAreas[1].AxisY.Title = "F(x)";

            int i = 0;
            foreach (ModelData item in selectedDataModels)
            {
                chart.Series.Add("Series" + i.ToString());
                chart.Series[i].Points.DataBindXY(item.grid_points, item.grid_func);
                chart.Series[i].ChartType = SeriesChartType.Spline;
                chart.Series[i].MarkerStyle = MarkerStyle.None;
                chart.Series[i].LegendText = item.p.ToString();
                chart.Series[i].Legend = "Lega";
                chart.Series[i].LegendText = "p = "+item.p.ToString();
                i++;
            }

            chart.Series.Add("Series" + i.ToString());
            List<double> p_List = new List<double>(selectedDataModels.Count);
            List<double> F_List = new List<double>(selectedDataModels.Count);
            foreach (ModelData item in selectedDataModels)
            {
                p_List.Add(item.p);
                F_List.Add(item.F(x));
            }
            chart.Series[i].ChartArea = "chartArea2";
            chart.Series[i].IsVisibleInLegend=false;
            chart.Series[i].Points.DataBindXY(p_List, F_List);
            chart.Series[i].ChartType = SeriesChartType.Spline;
            chart.Series[i].MarkerStyle = MarkerStyle.Circle;
            for (int j = 0; j < chart.Series[i].Points.Count; j++)
                chart.Series[i].Points[j].ToolTip = "p = " + chart.Series[i].Points[j].XValue.ToString(format) + "\nF(" + x + ") = " + chart.Series[i].Points[j].YValues[0].ToString(format);
        }

        public string Error { get { return "Error Text"; } }
        public string this[string property]
        {
            get
            {
                string message = null;
                switch (property)
                {
                    case "X":
                        if (x < 0 || x > 1)
                            message = "x must be less or equal " + 1 + " and greater or equal " + 0;
                        break;
                    default:
                        break;
                }
                return message;
            }
        }




    }
}
