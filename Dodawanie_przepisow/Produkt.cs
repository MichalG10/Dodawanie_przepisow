//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dodawanie_przepisow
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produkt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produkt()
        {
            this.Skladnik = new HashSet<Skladnik>();
        }
    
        public int Id_Produktu { get; set; }
        public int Id_Kategorii { get; set; }
        public string Nazwa { get; set; }
    
        public virtual Kategoria Kategoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Skladnik> Skladnik { get; set; }
    }
}