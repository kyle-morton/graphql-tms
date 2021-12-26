namespace GraphQLTMS.Shared.Domain
{
    public class Customer : EntityBase
    {
        public string Name { get; set; }
        public string CustomerNumber { get; set; }
        public decimal? SpendingLimit { get; set; }
        public bool IsOnHold { get; set; }
    }
}
