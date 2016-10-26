using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataContracts
{
    [DataContract]
    public class CsResultParkplatz
    {
        [DataMember]
        public int flag { get; set; }

        [DataMember]
        public List<CsParkplatz> Parkplatzs { get; set; }

        [DataMember]
        public string exception { get; set; }
    }
}
