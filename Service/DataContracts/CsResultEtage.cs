using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataContracts
{
    [DataContract]
    public class CsResultEtage
    {
        [DataMember]
        public int flag { get; set; }

        [DataMember]
        public List<int> Etages { get; set; }

        [DataMember]
        public string exception { get; set; }
    }
}
