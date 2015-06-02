using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniteEDTeacher.Code;

namespace UniteEDTeacher.Serialization
{
    public class ModuleSetting : AppSettings<ModuleSetting>
    {
        public String SettingName { get; set; }
        public String SettingData { get; set; }
    }
}
