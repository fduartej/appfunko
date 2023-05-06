using System.ComponentModel.DataAnnotations.Schema;

namespace appfunko.Models
{
    [Table("t_pago")]
    public class Pago
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? NombreTarjeta { get; set; }
        
        public string? NumeroTarjeta { get; set; }
        
        [NotMapped]
        public string? DueDateYYMM { get; set; }
        [NotMapped]
        public string? Cvv { get; set; }
        public Decimal MontoTotal{ get; set; }
        public string? UserID{ get; set; }
    }
}