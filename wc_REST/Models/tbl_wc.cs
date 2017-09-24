using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wc_REST.Models 
{
    public class tbl_wc : tbl_tbl_wc
    {
       public List<tbl_wcfile> tbl_wcfile;
        public List<tbl_comment> tbl_comment;
    }
}