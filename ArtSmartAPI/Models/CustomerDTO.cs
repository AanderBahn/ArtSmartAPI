using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ArtSmartAPI.Models
{
    [DataContract(Name = "Customer")]
    [Serializable]
    public class CustomerDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public Nullable<int> AddressId { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int PhoneNumber { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
    }
}