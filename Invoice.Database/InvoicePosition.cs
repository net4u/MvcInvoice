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
    
    public partial class InvoicePosition
    {
        public int Id { get; set; }
        public decimal AmountNetPln { get; set; }
        public decimal AmountNetCurrency { get; set; }
        public decimal AmountTaxPln { get; set; }
        public decimal AmountTaxCurrency { get; set; }
        public decimal AmountGrossPln { get; set; }
        public decimal AmountGrossCurrency { get; set; }
        public int TaxRateId { get; set; }
        public int InvoiceId { get; set; }
    
        public virtual TaxRate TaxRate { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}