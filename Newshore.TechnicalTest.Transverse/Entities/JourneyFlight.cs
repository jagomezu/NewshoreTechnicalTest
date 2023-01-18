using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newshore.TechnicalTest.Transverse.Entities
{
    [Table("JourneyFlight")]
    public class JourneyFlight
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("journeyId")]
        public int JourneyId { get; set; }

        [Column("flightId")]

        public int FlightId { get; set; }

    }
}
