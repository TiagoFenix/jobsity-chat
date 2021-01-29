using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsityChat.Models
{
    [Table("chatMessages")]
    public class ChatMessage
    {
        [Column("Id")]
        public long? Id { get; set; }

        [Column("Message")]
        public string Message { get; set; }

        [Column("Created")]
        public DateTime Created { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

    }
}
