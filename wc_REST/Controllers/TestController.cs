using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace wc_REST.Controllers
{
    public class TestController : ApiController
    {
        NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
        public object Get()
        {        
           return db.tbl_wc.OrderBy(c=>c.wc_longtitude).ThenBy(c=>c.wc_latitude).Take(25).ToList();
        }
    }
}
