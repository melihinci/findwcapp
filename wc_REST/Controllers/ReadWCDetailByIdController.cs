using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wc_REST.Helpers;
//using wc_REST.Models;

namespace wc_REST.Controllers
{
    public class ReadWCDetailByIdController : ApiController
    {
        [HttpGet]
        public object Get(Guid id)
        {
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            db.Configuration.LazyLoadingEnabled = true;
            db.Configuration.ProxyCreationEnabled = true;
           tbl_wc wcc =  db.tbl_wc.Find(id); 
            tbl_wc wc = JsonConvert.DeserializeObject<tbl_wc>(JsonConvert.SerializeObject(wcc));
            if (wc != null)
            {
                wc.tbl_comment = db.tbl_comment.Where(c => c.comment_wcid == wcc.wc_id).ToList();
                wc.tbl_wcfile = db.tbl_wcfile.Where(c => c.wcfile_wcid == wcc.wc_id).ToList();
                return wc;
            }
            else return new { Message = "wc not found!", Status = GenerelTypeStatus.FAILURE };
        }
    }
}
