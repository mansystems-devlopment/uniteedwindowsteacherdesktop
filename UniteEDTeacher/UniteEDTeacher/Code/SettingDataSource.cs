using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Forms;

namespace UniteEDTeacher.Code
{
    // SettingDataSource
    // Holds a collection of blog feeds (SettingData), and contains methods needed to
    // retreive the feeds.
    public class SettingDataSource
    {
        private ObservableCollection<ActivationModule> _ActivationModules = new ObservableCollection<ActivationModule>();

        
        public ObservableCollection<ActivationModule> ActivationModules
        {
            get
            {
                return this._ActivationModules;
            }
        }

        public SettingDataSource()
        {
            try
            {

                string json = (ModuleSetting.Load("AllModuleSetting")).SettingData;
                Newtonsoft.Json.Linq.JArray objs = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(json);

                foreach (Newtonsoft.Json.Linq.JObject obj in objs)
                {

                    ActivationModule SchoolAccountActivationModule = new ActivationModule();
                    SchoolAccountActivationModule.ModuleName = obj["ModuleName"].ToString();
                    SchoolAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(SchoolAccountActivationModule.ModuleName);
                    this.ActivationModules.Add(SchoolAccountActivationModule);
                }

            }
            catch (Exception ex) {
                
            }
        }

        // Returns the feed that has the specified title.
        public ActivationModule GetActivationModule(string title)
        {
            // Simple linear search is acceptable for small data sets
            var matches = this.ActivationModules.Where((activationModule) => activationModule.ModuleName.Equals(title));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

    }

}
