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

        // Combine the base folder with your specific folder....
        public static string specificFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mansystems South Africa\\UniteED\\");


        public void Save()
        {
            createFolder();

            string path = @specificFolder + DEFAULT_FILENAME + ".modulesetting";

            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }

         public void SaveStatus()
        {
            createFolder();

            string path = @specificFolder + DEFAULT_FILENAME + ".modulestatus";

            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }

        public static void Save(T pSettings, string fileName)
        {
            createFolder();
            string path = @specificFolder + fileName + ".modulesetting";
            File.WriteAllText(path,  JsonConvert.SerializeObject(pSettings));
        }

        public static T Load(string fileName)
        {
            T t = new T();
            string path = @specificFolder + fileName + ".modulesetting";

            if (File.Exists(path))
                t = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return t;
        }
        public static void createFolder(){
       

        // Check if folder exists and if not, create it
        if(!Directory.Exists(@specificFolder)) 
            Directory.CreateDirectory(@specificFolder);
        }
    }
}
