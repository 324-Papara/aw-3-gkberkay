namespace Para.Base.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public string InsertUser { get; set; } = "System";
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }

    }
}
