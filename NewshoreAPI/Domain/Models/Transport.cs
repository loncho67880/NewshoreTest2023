using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Transport")]
    public class Transport
    {
        [Key]
        public int Id { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
    }
}
