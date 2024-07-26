using System.ComponentModel.DataAnnotations.Schema;

namespace AssistanceQr.Models
{
    [Table(name: "Assistance")]
    public class Assistance
    {
        public Guid Id { get; set; }
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }

    }
}
