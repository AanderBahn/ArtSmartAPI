using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ArtSmartAPI.Models
{
    [DataContract(Name = "Cart")]
    [Serializable]
    public class CartDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int CartItemId { get; set; }
    }
}