//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RiverKeeperDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public string Response { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
