using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

namespace Framework
{
    public class ArchivingManager : ManagerBase<ArchivingManager>
    {
        BinaryFormatter bf = new BinaryFormatter();
        public void SaveFile(object obj,string path)
        {

            using(FileStream f = new FileStream(path,FileMode.OpenOrCreate))
            {
                bf.Serialize(f, obj);
            }
        }

        public T LoadFile<T>(string path) where T :class
        {
            T obj = null;
            if(File.Exists(path))
            {
                using (FileStream f = new FileStream(path, FileMode.OpenOrCreate))
                {
                    obj = bf.Deserialize(f) as T;
                }
                    
            }
            return obj;
        }
    }
}

