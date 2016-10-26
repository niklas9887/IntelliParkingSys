using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataContracts
{
    [DataContract]
    public class CsParkplatz
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Nummer { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Zustand { get; set; }

        [DataMember]
        public int Etage { get; set; }

        [DataMember]
        public int Parkplatzart_Id { get; set; }

        [DataMember]
        public List<CsKameraParkplatz> KameraParkplatzs { get; set; }

        [DataMember]
        public CsParkplatzart Parkplatzart  { get; set; }
    }
}
