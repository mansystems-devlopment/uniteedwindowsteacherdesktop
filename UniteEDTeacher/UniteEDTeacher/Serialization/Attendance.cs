using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    public class Attendance
    {
        public String SessionID { get; set; }
        public String Subject { get; set; }
        public String TeacherID { get; set; }
        public String StudentFullName { get; set; }
        public String AttendanceNote { get; set; }
        public String ResultCode { get; set; }
        public String ResultMessage { get; set; }
        public String Level { get; set; }
        public String TeacherFullName { get; set; }
        public String TimeLoggedIn { get; set; }
        public String Class_ { get; set; }
        public String StudentID { get; set; }
    }
}
