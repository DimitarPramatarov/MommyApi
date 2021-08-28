using System;

namespace MommyApi.Data.Models.Base
{
    public class Entity : IEntity
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set ; }
        public DateTime? ModifedOn { get; set ; }
        public string ModifiedBy { get; set; }
    }
}
