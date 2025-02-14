namespace MarketManagement.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        public void Activate()
        {
            this.IsActive = true;
            this.UpdatedAt = DateTime.Now;
        }

        public void Deactivate()
        {
            this.IsActive = false;
            this.UpdatedAt = DateTime.Now;
        }
    }
}
