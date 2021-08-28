namespace MommyApi.Data.Models.Base
{
    using System;

    public interface IEntity
    {
         DateTime CreatedOn { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifedOn{ get; set; }

        string ModifiedBy { get; set; }
    }
}
