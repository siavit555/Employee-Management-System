using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.UsersModels
{
    [DataContract]
    public class UserDetails
    {
        [DataMember(Name = "code")]
        public string StatusCode { get; set; } = String.Empty;

        [DataMember(Name = "meta")]
        public Meta? Meta { get; set; }

        [DataMember(Name = "data")]
        public IList<User>? Users { get; set; }

    }

    [DataContract]
    public class UserDetail
    {
        [DataMember(Name = "code")]
        public string? StatusCode { get; set; } = String.Empty;

        [DataMember(Name = "meta")]
        public Meta? Meta { get; set; }

        [DataMember(Name = "data")]
        public User? User { get; set; }

    }
}
