//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrderItem
    {
        public int ID { get; set; }
        public int FoodID { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
    
        public virtual tblMenu tblMenu { get; set; }
        public virtual tblOrder tblOrder { get; set; }
    }
}