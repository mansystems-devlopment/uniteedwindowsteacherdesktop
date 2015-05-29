using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UniteEDTeacher.Code
{
    // SettingDataSource
    // Holds a collection of blog feeds (SettingData), and contains methods needed to
    // retreive the feeds.
    public class SettingDataSource
    {
        private ObservableCollection<ActivationModule> _ActivationModules = new ObservableCollection<ActivationModule>();

        private ApplicationDataContainer Settings { get; set; }
        public ObservableCollection<ActivationModule> ActivationModules
        {
            get
            {
                return this._ActivationModules;
            }
        }

        public SettingDataSource(ApplicationDataContainer settings)
        {
            this.Settings = settings;

            ActivationModule storeActivationModule = new ActivationModule();
            storeActivationModule.ModuleName = "store";
            storeActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(this.Settings, storeActivationModule.ModuleName);
            this.ActivationModules.Add(storeActivationModule);

            ActivationModule booksActivationModule = new ActivationModule();
            booksActivationModule.ModuleName = "books";
            booksActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(this.Settings, booksActivationModule.ModuleName);
            this.ActivationModules.Add(booksActivationModule);

            ActivationModule GoogleAccountActivationModule = new ActivationModule();
            GoogleAccountActivationModule.ModuleName = "GoogleAccount";
            GoogleAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(this.Settings, GoogleAccountActivationModule.ModuleName);
            this.ActivationModules.Add(GoogleAccountActivationModule);

            ActivationModule MoodleAccountActivationModule = new ActivationModule();
            MoodleAccountActivationModule.ModuleName = "MoodleAccount";
            MoodleAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(this.Settings, MoodleAccountActivationModule.ModuleName);
            this.ActivationModules.Add(MoodleAccountActivationModule);

            ActivationModule CloudbancAccountActivationModule = new ActivationModule();
            CloudbancAccountActivationModule.ModuleName = "CloudbancAccount";
            CloudbancAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(this.Settings, CloudbancAccountActivationModule.ModuleName);
            this.ActivationModules.Add(CloudbancAccountActivationModule);

            ActivationModule SchoolAccountActivationModule = new ActivationModule();
            SchoolAccountActivationModule.ModuleName = "School";
            SchoolAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(this.Settings, SchoolAccountActivationModule.ModuleName);
            this.ActivationModules.Add(SchoolAccountActivationModule);


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
