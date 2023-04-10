using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.UsersModels
{
    [DataContract]
    public class Meta
    {
        [DataMember(Name = "pagination")]
        public Pagination? Pagination { get; set; }
    }
}
