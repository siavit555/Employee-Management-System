using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CommonEntities.UsersModels
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name"),Required]
        public string? Name { get; set; }

        [DataMember(Name = "email"), Required, DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DataMember(Name = "gender")]
        public string? Gender { get; set; }

        [DataMember(Name = "status")]
        public string? Status { get; set; }
    }
}