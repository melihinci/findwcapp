using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace wc_REST.Controllers
{
    public class GetUserByNameController : ApiController
    {
        
        public object Get(string name)
        {
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            db.Configuration.ProxyCreationEnabled = false;
            var user = db.tbl_user.Where(c => c.user_name == name).ToList();

       
            db.Configuration.ProxyCreationEnabled = true;
            return user;
        }

    }
}
