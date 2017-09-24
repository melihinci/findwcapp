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
    public class AddNewUserController : ApiController
    {

        public object Get(string surname ,string name)
        {
            if (name == string.Empty)
            {
                name = "Guest";
            }
            NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
      
            
                tbl_user user = new tbl_user
                {
                    user_name = name,
                    user_surname = surname,
                    user_banned = false,
                    user_deviceId ="123",
                    user_macAddress = "123",
                    user_id=Guid.NewGuid(),
                    user_mail="deneme@gmail.com",
                    user_photourl="url",
                    user_token="token"

                };
                db.tbl_user.Add(user);
                db.SaveChanges();
                return new { Message = "Success!", Status = GenerelTypeStatus.SUCCESS };
         
        }
    }
}