namespace MommyApi.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using MommyApi.Data.Models.Base;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser, IEntity
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifedOn { get; set; }
        public string ModifiedBy { get; set ; }

        public UserProfile UserProfile { get; set; }

        public IEnumerable<Post> Post { get; } = new HashSet<Post>();
    }
}
