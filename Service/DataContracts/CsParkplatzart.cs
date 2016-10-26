using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataContracts
{
    [DataContract]
    public class CsParkplatzart
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Art { get; set; }
    }
}
