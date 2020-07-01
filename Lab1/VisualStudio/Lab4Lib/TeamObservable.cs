using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Lab4Lib
{
    [Serializable]
    public class TeamObservable : ObservableCollection<Person>
    {
        //[field: NonSerialized]
        //new public event PropertyChangedEventHandler OnPropertyChanged;

        private string name { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        private List<string> topics = new List<string>();
        public List<string> Topics
        {
            get { return topics; }
        }
        public TeamObservable(string name="Test", params string[] research_cases)
        {
            this.name = name;

            this.Change = false;
 
            int f;
            foreach (var new_pers in research_cases)
            {
                f = 0;
                foreach (var exist_pers in topics)
                {
                    if (new_pers == exist_pers)
                    {
                        f = 1;
                        break;
                    }
                }
                if (f == 0)
                {
                    topics.Add(new_pers);
                }
            }

            this.Percent = 0;

        }

        private double percent;
        public double Percent
        {
            get
            {
                return percent;
            }
            set 
            {
                percent = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Percent"));

            } 
        }

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
        public void AddPerson(params Person[] person)
        {
            int f;
            foreach (var i in person)
            {
                f = 0;
                foreach (var j in base.Items)
                {
                    if (i == j)
                    {
                        f = 1;
                        break;
                    }
                }
                if (f == 0)
                {
                    base.Add((Person)i.DeepCopy());
                }
            }
        }

        public void RemovePersonAt(int index)
        {
            base.Remove(base[index]);
        }
        public void AddDefaults()
        {
            base.Add(new Person());
            base.Add(new Programmer());
            base.Add(new Researcher());
        }
        public void AddDefaultProgrammer()
        {
            base.Add(new Programmer());
        }
        public void AddDefaultResearcher()
        {
            base.Add(new Researcher());
        }

        public override string ToString()
        {
            string str = name;

            foreach (string i in topics)
            {
                str += "; " + i;
            }
            foreach (Person i in base.Items)
            {
                str += "; " + i.ToString();
            }
            return str;
        }

        public bool FilterByResearcher(object pers)
        {
            Person person = pers as Person;
            return (person is Researcher);
        }

        public static bool Save(string filename, TeamObservable obj)
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

        public static bool Load(string filename, ref TeamObservable obj)
        {
            bool b = true;
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                obj = binaryFormatter.Deserialize(fileStream) as TeamObservable;
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
