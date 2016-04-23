using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ArtSmartAPI.Models
{
    [DataContract(Name = "Resolution")]
    [Serializable]
    public class ResolutionDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Measurement { get; set; }
        [DataMember]
        public string Size { get; set; }
    }
}