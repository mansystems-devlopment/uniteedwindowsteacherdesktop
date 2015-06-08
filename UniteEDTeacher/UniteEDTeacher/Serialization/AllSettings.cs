using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniteEDTeacher.Code;

namespace UniteEDTeacher.Serialization
{
    public class AllSettings : AppSettings<AllSettings>
    {
        public String AllModuleSetting { get; set; }
    }
}
