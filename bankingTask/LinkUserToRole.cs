//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bankingTask
{
    using System;
    using System.Collections.Generic;
    
    public partial class LinkUserToRole
    {
        public string UserCode { get; set; }
        public int RoleCode { get; set; }
        public byte[] RowVer { get; set; }
        public string OnCreatedBy { get; set; }
        public Nullable<System.DateTime> OnCreated { get; set; }
        public string OnUpdatedBy { get; set; }
        public Nullable<System.DateTime> OnUpdated { get; set; }
    
        public virtual Role Role { get; set; }
    }
}