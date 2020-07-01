using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Lib
{
    interface IDeepCopy { object DeepCopy(); }

    [Serializable]
    public class Person : IDeepCopy
    {
        public string[] Name { get; set; }
        public System.DateTime Date { get; set; }

        public Person(string N = "Danny", string L = "Jones", DateTime d = new DateTime())
        {
            this.Name = new string[2];
            this.Name[0] = N;
            this.Name[1] = L;
            this.Date = d;
        }

        public override string ToString()
        {
            return Name[0] + ' ' + Name[1] + ' ' + Date.ToShortDateString();
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    Person P = obj as Person;
        //    if (P is null) return false;
        //    return P.Name[0] == this.Name[0] && P.Name[1] == this.Name[1] && P.Date == this.Date;
        //}

        //public static bool operator ==(Person one, Person two)
        //{
        //    if (ReferenceEquals(one, null) || ReferenceEquals(one, null))
        //        return ReferenceEquals(one, two);
        //    return one.Name[0] == two.Name[0] && one.Name[1] == two.Name[1] && one.Date == two.Date;
        //}

        //public static bool operator !=(Person one, Person two)
        //{
        //    return !(one == two);
        //}
        //public override int GetHashCode()
        //{
        //    return this.Name[0].GetHashCode() + this.Name[1].GetHashCode() + this.Date.GetHashCode();
        //}

        public virtual object DeepCopy()
        {
            Person P = new Person();
            P.Name[0] = this.Name[0];
            P.Name[1] = this.Name[1];
            P.Date = this.Date;
            return P;

        }

    }
}
