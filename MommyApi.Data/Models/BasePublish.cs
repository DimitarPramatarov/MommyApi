namespace MommyApi.Data.Models
{
    using System;
    using MommyApi.Data.Models.Base;

    public abstract class BasePublish : IDeletableEntity
    {
        public string Description { get; set; }

        public int Votes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
