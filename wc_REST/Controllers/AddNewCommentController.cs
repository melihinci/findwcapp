using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using wc_REST.Helpers;

namespace wc_REST.Controllers
{
    public class AddNewCommentController : ApiController
    {

        public object Get( Guid wcid, Guid userid, string comment ,string name)
        {
            if (name == string.Empty)
            {
                name = "Guest";
            }
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            var wc = db.tbl_wc.Find(wcid);
            if (wc != null)
            {
                tbl_comment com = new tbl_comment
                {
                    comment_id = Guid.NewGuid(),
                    comment_text = comment, 
                    comment_date = DateTime.Now 
                };
                db.tbl_comment.Add(com);
                db.SaveChanges();
                return new { Message = "Success!", Status = GenerelTypeStatus.SUCCESS };
            }
            else
            {
                return new { Message = "wc not found!", Status = GenerelTypeStatus.FAILURE };
            }
        }
    }
}