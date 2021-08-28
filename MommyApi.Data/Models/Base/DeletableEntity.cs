namespace MommyApi.Data.Models.Base
{
    using System;

    public class DeletableEntity : IDeletableEntity
    {
        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
