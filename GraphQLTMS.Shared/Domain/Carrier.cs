namespace GraphQLTMS.Shared.Domain
{
    public class Carrier : EntityBase
    {
        public string Name { get; set; }
        public string Scac { get; set; }
        public string SafetyRating { get; set; }
        public bool IsApproved { get; set; }
    }
}
