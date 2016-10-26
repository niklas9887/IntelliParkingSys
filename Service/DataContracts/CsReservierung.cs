using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataContracts
{
    [DataContract]
    public class CsReservierung
    {
       [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Datum { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Nummernschild { get; set; }

        [DataMember]
        public int Parkplatz_Id { get; set; }

        [DataMember]
        public  CsParkplatz Parkplatz{ get; set; }
    }
}
