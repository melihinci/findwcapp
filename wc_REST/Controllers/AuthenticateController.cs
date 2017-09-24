using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class AuthenticateController : ApiController
    {
        [HttpPost]
        public object Get()
        {
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
            System.IO.StreamReader streamReader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
            streamReader.BaseStream.Position = 0;
            string requestFromPost = streamReader.ReadToEnd();
            AuthenticationRequestType authenticationrequesttype;
            
                  authenticationrequesttype = JsonConvert.DeserializeObject<AuthenticationRequestType>(requestFromPost);
            if (authenticationrequesttype == null)
            {
                return new AuthenticationRequestType { user_mail = "gplus accountun bağlı olduğu mail", user_name = "nullable kullanıcı adı", gplustoken = "googleın verdiği token" };
            }

            var user = db.tbl_user.FirstOrDefault(c => c.user_mail == authenticationrequesttype.user_mail);
            if (user != null)
            {
                if (user.user_banned)
                {
                    return new { Status = GenerelTypeStatus.OK, Message = "Banned" };
                }
                else
                {
                    var result = new { Status = GenerelTypeStatus.OK, Message = user.user_id};
                    return result;
                }
            }
            else
            {
                string surname = "";
                string name = "";
                if (authenticationrequesttype.user_name.Contains(" "))
                { 
                        surname = authenticationrequesttype.user_name.Split(' ').ToList().Last();
                        name = authenticationrequesttype.user_name.Split(' ')[0];
                        if (authenticationrequesttype.user_name.Split(' ').Length > 2)
                        {
                            name = name + " " + authenticationrequesttype.user_name.Split(' ')[1];
                        } 
                }
                tbl_user usr = new tbl_user
                {
                    user_id = Guid.NewGuid(),
                    user_deviceId = "",
                    user_macAddress = "",
                    user_banned = false,
                    user_mail = authenticationrequesttype.user_mail,
                    user_name = name,
                    user_photourl = "",
                    user_surname = surname,
                    user_token = ""
                };
                db.tbl_user.Add(usr);
                db.SaveChanges();
                var result = new { Status = GenerelTypeStatus.OK, Message = usr.user_id};
                return result;
            }


        }
        
    }
}
