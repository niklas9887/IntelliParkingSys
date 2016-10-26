using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataContracts
{
    [DataContract]
    public class CsKamera
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Etage { get; set; }

        [DataMember]
        public string Sonstiges { get; set; }

        [DataMember]
        public List<CsKameraParkplatz> KameraParkplatzs { get; set; }
    }
}
