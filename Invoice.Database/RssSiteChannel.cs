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
    
    public partial class RssSiteChannel
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int RssChannelId { get; set; }
    
        public virtual RssChannel_SDIC RssChannel_SDIC { get; set; }
    }
}