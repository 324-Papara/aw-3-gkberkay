﻿using Para.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Para.Data.Domain
{
    [Table("CustomerDetail", Schema = "dbo")]
    public class CustomerDetail : BaseEntity
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EducationStatus { get; set; }
        public string MountlyIncome { get; set; }
        public string Occupation { get; set; }


    }

}
