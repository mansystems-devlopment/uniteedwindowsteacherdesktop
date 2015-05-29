using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    public class TeacherInfo : URSResponse
    {
        public List<LevelInfo> OutTeacherInfo_Out_Level { get; set; }
        public List<SubjectInfo> OutTeacherInfo_OutSubjects { get; set; }
        public List<ClassInfo> OutTeacherInfo_OutClasses { get; set; }
        public String Class_ { get; set; }
    }
}
