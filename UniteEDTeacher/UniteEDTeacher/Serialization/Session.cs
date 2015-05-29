using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    public class Session:URSResponse
    {
        public String SessionID { get; set; }
        public String LoginTime { get; set; }
    }
}
