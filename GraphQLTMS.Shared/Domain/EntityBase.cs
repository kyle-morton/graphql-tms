namespace GraphQLTMS.Shared.Domain
{
    public class EntityBase
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime? CreatedByDate { get; set; }
        public string UpdatedByUserId { get; set; }
        public DateTime? UpdatedByDate { get; set; }
    }
}
