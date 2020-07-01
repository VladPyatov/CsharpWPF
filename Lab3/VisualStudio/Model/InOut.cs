using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public interface ISerialize
    {
        bool Serialize<T>(T obj) where T:class;
    }

    public class SerializeAdapter : ISerialize
    {
        private FileStream file;

        public SerializeAdapter(FileStream f)
        {
            file = f;
        }

        public bool Serialize<T>(T obj) where T:class
        {
            bool b = true;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, obj);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                b = false;
            }
            finally
            { if (file != null) file.Close(); }

            return b;
            
        }
    }

    public interface IDeserialize
    {
        bool Deserialize<T>(ref T obj) where T: class;
    }

    public class DeserializeAdapter : IDeserialize
    {
        private FileStream file;

        public DeserializeAdapter(FileStream f)
        {
            file = f;
        }

        public bool Deserialize<T>(ref T obj) where T:class
        {
            bool b = true;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                obj = binaryFormatter.Deserialize(file) as T;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                b = false;
            }
            finally
            { if (file != null) file.Close(); }

            return b;

        }
    }
}
