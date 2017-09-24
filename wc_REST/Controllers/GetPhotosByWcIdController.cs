using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace wc_REST.Controllers
{
    public class GetPhotosByWcIdController : ApiController
    {
         
        public object Get(Guid wc_id)
        {
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            db.Configuration.ProxyCreationEnabled = false;
            var wc = db.tbl_wc.Find(wc_id);
            if (wc == null)
            {
                return new { none = "none" };
            } 
            var result = wc.tbl_wcfile.ToArray();
            db.Configuration.ProxyCreationEnabled = true;
            return result;
        }
    }
}
