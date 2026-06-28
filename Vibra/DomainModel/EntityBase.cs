namespace DomainModel
{
    public class EntityBase
    {

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
