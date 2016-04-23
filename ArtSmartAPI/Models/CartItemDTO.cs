using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ArtSmartAPI.Models
{
    [DataContract(Name = "CartItem")]
    [Serializable]
    public class CartItemDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CartId { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
    }
}