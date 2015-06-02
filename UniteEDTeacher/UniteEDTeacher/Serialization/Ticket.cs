using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    public class Ticket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DateIssued { get; set; }
        public string IHave { get; set; }
        public string WithMy { get; set; }
        public string Description { get; set; }
        public string TicketNumber { get; set; }
    }
}
