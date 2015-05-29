using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniteEDTeacher.Serialization
{
    [Table("Ticket")]
    public class Ticket
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DateIssued { get; set; }
        public string IHave { get; set; }
        public string WithMy { get; set; }
        public string Description { get; set; }
        public string TicketNumber { get; set; }
    }
}
