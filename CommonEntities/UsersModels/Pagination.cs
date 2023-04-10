using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.UsersModels
{
    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "pages")]
        public int Pages { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
