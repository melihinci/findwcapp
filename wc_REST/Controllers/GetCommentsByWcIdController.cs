using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace wc_REST.Controllers
{
    public class GetCommentsByWcIdController : ApiController
    {
        
        public object Get(Guid wc_id)
        {
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            db.Configuration.ProxyCreationEnabled = false;
            var wc = db.tbl_comment.Where(c => c.comment_wcid == wc_id).ToList();

            var wc_test = db.tbl_comment.ToList();
            if (wc == null)
            {
                return new { none = "none" };
            }
            var result = wc.ToArray();
            db.Configuration.ProxyCreationEnabled = true;
            return result;
        }

    }
}
