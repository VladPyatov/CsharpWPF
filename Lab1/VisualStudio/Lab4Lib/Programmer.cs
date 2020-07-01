using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4Lib
{
    [Serializable]
    public class Programmer: Person, IDeepCopy, IComparable<Programmer>,IComparable
    {
        public double Exp { get; set; }
        public string Topic { get; set; }

        public Programmer(string N = "Tom", string L = "Fletcher", DateTime d = new DateTime(), string T = "C#", double e = 0)
        {
            this.Topic = T; 
            this.Exp = e; 
            this.Name[0] = N;
            this.Name[1] = L;
            this.Date = d;
        }
        public override object DeepCopy()
        {
            Programmer P = new Programmer();
            P.Name[0] = this.Name[0];
            P.Name[1] = this.Name[1];
            P.Date = this.Date;
            P.Exp = this.Exp;
            P.Topic = this.Topic;
            return P;

        }
        public int CompareTo(Programmer other)
        {
            return this.Exp.CompareTo(other.Exp);
        }
        public int CompareTo(object other)
        {
            return this.Exp.CompareTo((other as Programmer).Exp);
        }
        public override string ToString()
        {
            return Name[0] + " " + Name[1] + " " + Date.ToShortDateString() + " " + Topic + " " + Exp;
        }
    }
}
