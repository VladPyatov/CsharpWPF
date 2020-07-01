using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4Lib
{
    class Team: IDeepCopy
    {
        public string Group { get; set; } //Название группы
        public List<Person> Topic { get; set; } //Список участников

        public Team(string g)
        {
            this.Group = g;
            this.Topic = new List<Person>();
        }

        public void AddPerson(params Person[] person)
        {
            int f;
            foreach (var i in person)
            {
                f = 0;
                foreach (var j in Topic)
                {
                    if (i == j)
                    {
                        f = 1;
                        break;
                    }
                }
                if(f == 0)
                {
                    Topic.Add((Person)i.DeepCopy());
                }
            }
        }

        public void AddDefaults()
        {
            Topic.Add(new Person());
            Topic.Add(new Programmer());
            Topic.Add(new Researcher());
        }

        public bool IsProgrammer(Person ps)
        {
            if (ps is Programmer)
                return true;
            else return false;
        }

        public override string ToString()
        {
            string str = Group;
            foreach(Person i in Topic)
            {
                str += "; " + i.ToString();
            }
            return str;
        }
        public virtual object DeepCopy()
        {
            Team P = new Team(this.Group);
            foreach (Person pers in this.Topic)
            {
                P.AddPerson(pers);
            }
            return P;

        }

        public IEnumerable<Person> Subset(Predicate<Person> Filter)
        {
            foreach(Person pers in Topic)
            {
                if (Filter(pers))
                    yield return pers;
            }
        }

        public IEnumerable<string> NonUniqueSubject()
        {
            List<string> s = new List<string>();
            List<string> printed= new List<string>();
            foreach (Person pers in Topic)
            {
                if (pers is Programmer) 
                {
                    if (s.IndexOf((pers as Programmer).Topic) > -1) 
                    {
                        if (printed.IndexOf((pers as Programmer).Topic) == -1)
                        {
                            printed.Add((pers as Programmer).Topic);
                            yield return (pers as Programmer).Topic;
                        }

                    }
                    else
                    {
                        s.Add((pers as Programmer).Topic);
                    }
                }

            }
        }
    }
}
