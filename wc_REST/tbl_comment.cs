//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wc_REST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tbl_comment
    {
        public string comment_text { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Guid comment_id { get; set; }
        public System.Guid comment_userid { get; set; }
        public System.Guid comment_wcid { get; set; }
        public System.DateTime comment_date { get; set; }
    
        public  tbl_user tbl_user { get; set; }
        public  tbl_wc tbl_wc { get; set; }
    }
}
