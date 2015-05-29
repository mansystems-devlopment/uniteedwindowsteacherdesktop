using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    class LoginResponse : URSResponse
    {
        public String FirstName { get;set; }
        public String Surname { get; set; }       
    }
}
