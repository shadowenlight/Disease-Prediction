//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftwareProject.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class DAppDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DAppDate()
        {
            this.Appointment = new HashSet<Appointment>();
        }
    
        public int Id { get; set; }
        public string Time { get; set; }
        public Nullable<int> Doctor_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}