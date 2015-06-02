using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniteEDTeacher.Code
{
    public class AppSettings<T> where T : new()
    {
        public string DEFAULT_FILENAME = "settings";

        public void Save()
        {
            File.WriteAllText(DEFAULT_FILENAME, JsonConvert.SerializeObject(this));
        }

        public static void Save(T pSettings, string fileName)
        {
            File.WriteAllText(fileName,  JsonConvert.SerializeObject(pSettings));
        }

        public static T Load(string fileName)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}
