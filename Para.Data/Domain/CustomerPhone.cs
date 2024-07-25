using Para.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Para.Data.Domain
{
    [Table("CustomerPhone", Schema = "dbo")]

    public class CustomerPhone : BaseEntity
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string CountryCode { get; set; } //TUR or 90
        public string Phone { get; set; }
        public bool IsDefault { get; set; }
    }
}
