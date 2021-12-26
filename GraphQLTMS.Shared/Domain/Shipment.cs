using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLTMS.Shared.Domain
{
    public class Shipment : EntityBase
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int WeightInPounds { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal Revenue { get; set; }
        public decimal Cost { get; set; }
        /// <summary>
        /// Not Mapped
        /// </summary>
        [NotMapped]
        public decimal GP => Revenue - Cost;
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? CarrierId { get; set; }
        public Carrier Carrier { get; set; }
    }
}
