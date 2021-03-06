//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParkplatzSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParkplatzSet()
        {
            this.KameraParkplatzSets = new HashSet<KameraParkplatzSet>();
            this.ReservierungSets = new HashSet<ReservierungSet>();
        }
    
        public int Id { get; set; }
        public int Nummer { get; set; }
        public string Name { get; set; }
        public int Etage { get; set; }
        public int Parkplatzart_Id { get; set; }
        public string Zustand { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KameraParkplatzSet> KameraParkplatzSets { get; set; }
        public virtual ParkplatzartSet ParkplatzartSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservierungSet> ReservierungSets { get; set; }
    }
}
