using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AssistanceQr.Models
{

    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        public string PaternalSurname { get; set; } = string.Empty;
        public string MaternalSurname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Phonenumber { get; set; } = string.Empty;
        public DNIType DNIType { get; set; }
        public string DNI { get; set; } = string.Empty;

        public Sex Sex { get; set; }
        public ParticipantType ParticipantType { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
    public enum ParticipantType
    {
        ESTUDIANTE = 1,
        DOCENTE = 2,
        EGRESADO = 3,
        OTROS = 4
    }
    public enum Sex
    {
        Masculino = 1,
        Femenino = 2
    }
    public enum DNIType
    {
        CE = 2,
        DNI = 1
    }
}
