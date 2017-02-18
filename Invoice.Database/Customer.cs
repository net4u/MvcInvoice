//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Invoice.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Invoice = new HashSet<Invoice>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Nip { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public Nullable<int> BankAccountId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string DataSearch { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual Company Company { get; set; }
    }
}
