using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Lib
{
    [Serializable]
    public class Researcher : Person, IDeepCopy, IComparable<Researcher>, IComparable
    {
        public string Topic { get; set; }
        public int num { get; set; }

        public Researcher(string N = "Harry", string L = "Judd", DateTime d = new DateTime(), string T = "Math", int n = 3)
        {
            this.Topic = T; 
            this.num = n;
            this.Name[0] = N;
            this.Name[1] = L;
            this.Date = d;
        }
        public override string ToString()
        {
            return Name[0] + ' ' + Name[1] + ' ' + Date.ToShortDateString() + ' ' + Topic + ' ' + num;
        }
        public override object DeepCopy()
        {
            Researcher R = new Researcher();
            R.Name[0] = this.Name[0];
            R.Name[1] = this.Name[1];
            R.Date = this.Date;
            R.num = this.num;
            R.Topic = this.Topic;
            return R;

        }
        public int CompareTo(Researcher other)
        {
            return this.num.CompareTo(other.num);
        }
        public int CompareTo(object other)
        {
            return this.num.CompareTo((other as Researcher).num);
        }
    }
}
