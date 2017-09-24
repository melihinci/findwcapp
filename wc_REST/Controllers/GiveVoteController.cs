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
    public class GiveVoteController : ApiController
    {

        public object Get( Guid wcid, Guid userid, int vote)
        {
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            var wc = db.tbl_wc.Find(wcid);
            if (wc != null)
            {
                tbl_score score = new tbl_score
                {
                    score_id = Guid.NewGuid(),
                    score_wcid = wc.wc_id,
                    user_id = userid,
                    score = vote
                };
                db.tbl_score.Add(score);
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