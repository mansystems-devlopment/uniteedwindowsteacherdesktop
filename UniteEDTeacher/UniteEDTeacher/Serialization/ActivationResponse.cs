using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    class ActivationResponse : URSResponse
    {
        public String ClientID { get; set; }
        public String Token { get; set; }
        public String Activated { get; set; }
        public String DivisionName { get; set; }
        public string TicketNumber { get; set; }
        public List<ActivationModule> OutActivateUser_ModuleList { get; set; }
        public List<ModuleStatus> OutAppStatus_OutApps { get; set; }
        
    }
}
