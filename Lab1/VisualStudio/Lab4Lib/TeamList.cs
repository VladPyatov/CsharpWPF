using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab4Lib
{
    class TeamList
    {
        public List<Team> TList { get; set; }

        public TeamList()
        {
            this.TList = new List<Team>();
        }
        public void AddDefaults()
        {
            Team t1 = new Team("MSU");
            t1.AddPerson(new Person("Daniil", "Andrianov", new DateTime(1999, 3, 3)));
            t1.AddPerson(new Person("Tom", "Fletcher", new DateTime(1999, 3, 13)));
            t1.AddPerson(new Programmer("Vlad", "Pyatov", new DateTime(1999, 3, 5), "C++", 5));
            t1.AddPerson(new Researcher("Harry", "Styles", new DateTime(1999, 9, 30), "C#", 2));

            Team t2 = new Team("CMC");
            t2.AddPerson(new Person("Tom", "Fletcher", new DateTime(1999, 3, 13)));
            t2.AddPerson(new Person("Daniil", "Andrianov", new DateTime(1999, 3, 3)));
            t2.AddPerson(new Programmer("Danny", "Jones", new DateTime(1999, 5, 11), "C#", 0));
            t2.AddPerson(new Programmer("D", "J", new DateTime(1999, 5, 11), "CC", 0));
            t2.AddPerson(new Researcher("Dougie", "Pointer", new DateTime(1999, 10, 12), "C", 5));
            t2.AddPerson(new Programmer("C", "3PO", new DateTime(1972, 5, 11), "C", 10));

            Team t3 = new Team("MM");
            t3.AddPerson(new Person("One", "Two", new DateTime(2001, 1, 1)));
            t3.AddPerson(new Person("Daniil", "Andrianov", new DateTime(1999, 3, 3)));
            t3.AddPerson(new Programmer("R2", "D2", new DateTime(1972, 5, 11), "CC", 10));
            t3.AddPerson(new Researcher("James", "Bourne", new DateTime(1999, 10, 12), "C++", 3));

            TList.Add(t1);
            TList.Add(t2);
            TList.Add(t3);
        }

        public int MaxPub
        {
            get
            {
                Researcher r = null;
                int d = (from team in TList from item in team.Topic where item is Researcher select item as Researcher).Count();
                if (d == 0)
                    return -1;
                else
                    r= (from team in TList from item in team.Topic where item is Researcher select item as Researcher).Max();
                return r.num;
            }
        }

        public Researcher MaxR
        {
            get
            {
                Researcher r = null;
                int d = (from team in TList from item in team.Topic where item is Researcher select item as Researcher).Count();
                if (d == 0)
                    return null;
                else
                    r = (from team in TList from item in team.Topic where item is Researcher select item as Researcher).Max();
                return r;
            }
        }

        public IEnumerable<Programmer> ExpProg
        {
            get
            {
                IEnumerable<Programmer> d = from team in TList from item in team.Topic 
                                            where item is Programmer orderby (item as Programmer).Exp 
                                            select (item as Programmer);
                return d;
            }
        }

        public IEnumerable<IGrouping<double, Programmer>> GrProg
        {
            get
            {
                IEnumerable<IGrouping<double, Programmer>> d = from team in TList from item in team.Topic 
                                                               where item is Programmer orderby (item as Programmer).Exp 
                                                               group(item as Programmer) by (item as Programmer).Exp;
                return d;
            }
        }

        public IEnumerable<Person> AllPers
        {
            get
            {
                IEnumerable<Person> d = (from team1 in TList
                                        from team2 in TList
                                        from item1 in team1.Topic
                                        from item2 in team2.Topic
                                        where !(item1 is Researcher) && !(item2 is Researcher) && !(item1 is Programmer) && !(item2 is Programmer) && !ReferenceEquals(team1.Topic, team2.Topic) && (item1 == item2)
                                        select item1).Distinct();
                return d;
            }
        }

        public IEnumerable<string> AllTopics
        {
            get
            {
                IEnumerable<string> d = (from team1 in TList
                                        from team2 in TList
                                        from item1 in team1.Topic
                                        from item2 in team2.Topic
                                        where (item1 is Researcher) && (item2 is Programmer) && (item1 as Researcher).Topic == (item2 as Programmer).Topic
                                        select (item1 as Researcher).Topic).Distinct();
                return d;
            }
        }
        public override string ToString()
        { 
            string str="";
            foreach (Team i in TList)
            {
                str += i.ToString()+".\n";
            }
            return str;
        }

    }
}
