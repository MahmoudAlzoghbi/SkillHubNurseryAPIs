//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NurseryDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kid
    {
        public int id { get; set; }
        public string name { get; set; }
        public double age { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool approve { get; set; }
        public bool collected { get; set; }
        public string image { get; set; }
        public System.DateTime date_of_birth { get; set; }
        public System.DateTime created_on { get; set; }
        public int nursery_id { get; set; }
    }
}
