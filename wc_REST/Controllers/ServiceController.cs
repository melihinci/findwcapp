//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Validation;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;
//using System.Web.Http;
//using wc_REST.Areas.Interfaces;
//using wc_REST.Helpers;

//namespace wc_REST.Controllers
//{
//    public class ServiceController : ApiController
//    {
//        NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
//        public object AddNewComment(Guid wcid, Guid userid, string comment)
//        {
//            var wc = db.tbl_tbl_wc.Find(wcid);
//            if (wc != null)
//            {
//                tbl_comment com = new tbl_comment
//                {
//                    comment_id = Guid.NewGuid(),
//                    comment_text = comment,
//                    comment_userid = userid,
//                    comment_wcid = wcid,
//                    comment_date = DateTime.Now
//                };
//                db.tbl_comment.Add(com);
//                db.SaveChanges();
//                return new { Message = "Success!", Status = GenerelTypeStatus.SUCCESS };
//            }
//            else
//            {
//                return new { Message = "wc not found!", Status = GenerelTypeStatus.FAILURE };
//            }
//        }

//        public object AddNewPhoto(Guid wcid, Guid userid)
//        {
//            if (wcid == null)
//                return new { Message = "wcid is missing", Status = GenerelTypeStatus.FAILURE };

//            var wc = db.tbl_tbl_wc.Find(wcid);
//            if (wc == null)
//                return new { Message = "wrong wcid", Status = GenerelTypeStatus.FAILURE };
//            if (userid == null)
//                return new { Message = "userid is missing", Status = GenerelTypeStatus.FAILURE };

//            var user = db.tbl_user.Find(userid);

//            if (user == null)
//                return new { Message = "wrong userid", Status = GenerelTypeStatus.FAILURE };
//            using (var reader = new StreamReader(HttpContext.Current.Request.Files[0].InputStream))
//            {
//                db.tbl_wcfile.Add(new tbl_wcfile
//                {
//                    wcfile_data = reader.ReadToEnd(),
//                    wcfile_date = DateTime.Now,
//                    wcfile_id = Guid.NewGuid(),
//                    wcfile_userid = userid,
//                    wcfile_wcid = wcid
//                });
//            }
//            db.SaveChanges();
//            return new { Status = GenerelTypeStatus.SUCCESS, Message = "success" };
//        }

//        public object GiveRate(Guid wcid, Guid userid, int score)
//        {
//            tbl_tbl_wc wc = db.tbl_tbl_wc.Find(wcid);
//            if (wc != null)
//            {
//                var existRecord = db.tbl_score.FirstOrDefault(c => c.score_wcid == wc.wc_id && c.user_id == userid);
//                if (existRecord != null)
//                {
//                    existRecord.score = (short)score;
//                }
//                else
//                {
//                    db.tbl_score.Add(new tbl_score
//                    {
//                        score = (short)score,
//                        score_id = Guid.NewGuid() 
//                    });
//                }
//                db.SaveChanges();
//                return new { Message = "success", Status = GenerelTypeStatus.SUCCESS };
//            }
//            else
//            {
//                return new { Message = "wc not found", Status = GenerelTypeStatus.FAILURE };
//            }
//        }

//        public object InsertNewWC()
//        {
//            System.IO.StreamReader streamReader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
//            streamReader.BaseStream.Position = 0;
//            string requestFromPost = streamReader.ReadToEnd();
//            tbl_tbl_wc wc = JsonConvert.DeserializeObject<tbl_tbl_wc>(requestFromPost);
//            if (wc != null)
//            {
//                try
//                {
//                    var tbl_wc = new tbl_tbl_wc();
//                    tbl_wc.wc_id = Guid.NewGuid();
//                    tbl_wc.wc_ablution = wc.wc_ablution;
//                    tbl_wc.wc_approved = true;
//                    tbl_wc.wc_babystop = wc.wc_babystop;
//                    tbl_wc.wc_traditional = wc.wc_traditional;
//                    tbl_wc.wc_european = wc.wc_european;
//                    tbl_wc.wc_fordisabled = wc.wc_fordisabled;
//                    tbl_wc.wc_foreignid = wc.wc_foreignid.Replace("-", "").Trim();
//                    tbl_wc.wc_genderfemale = wc.wc_genderfemale;
//                    tbl_wc.wc_gendermale = wc.wc_gendermale;
//                    tbl_wc.wc_latitude = wc.wc_latitude;
//                    tbl_wc.wc_longtitude = wc.wc_longtitude;
//                    tbl_wc.wc_music = wc.wc_music;
//                    try
//                    {
//                        if (wc.wc_name == string.Empty)
//                        {
//                            tbl_wc.wc_name = ReverseGeoCode(wc.wc_latitude.ToString(), wc.wc_longtitude.ToString());
//                        }
//                        else
//                        {
//                            tbl_wc.wc_name = wc.wc_name;
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        tbl_wc.wc_name = "İsim Bulunamadı.";
//                    }
//                    tbl_wc.wc_createddate = DateTime.Now;
//                    tbl_wc.wc_paper = wc.wc_paper;
//                    tbl_wc.wc_score = 20;
//                    tbl_wc.wc_acpower = wc.wc_acpower;
//                    tbl_wc.wc_wifi = wc.wc_wifi;
//                    tbl_wc.wc_inappropriatereport = 0;
//                    tbl_wc.wc_notexistreport = 0;
//                    tbl_wc.wc_type = "6";
//                    tbl_wc.wc_paid = wc.wc_paid;
//                    db.tbl_tbl_wc.Add(tbl_wc);
//                    db.SaveChanges();
//                    //  Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(new Exception(new JavaScriptSerializer().Serialize(tbl_wc))));

//                    return new
//                    {
//                        Status = GenerelTypeStatus.SUCCESS,
//                        Message = ""
//                    };
//                }
//                catch (Exception e)
//                {
//                    //  Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));

//                    return new
//                    {
//                        Status = GenerelTypeStatus.FAILURE,
//                        Message = "Check Elmah"
//                    };
//                }
//                //catch (DbEntityValidationException e)
//                //{

//                //    foreach (var eve in e.EntityValidationErrors)
//                //    {
//                //        string s=string.Format("Entity türü \"{0}\" şu hatalara sahip \"{1}\" Geçerlilik hataları:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                //        foreach (var ve in eve.ValidationErrors)
//                //        {
//                //            s=s+(string.Format("- Özellik: \"{0}\", Hata: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
//                //        } 
//                //    }
//                //}
//                //return null;
//            }
//            else
//            {
//                return new
//                {
//                    Status = GenerelTypeStatus.FAILURE,
//                    Message = "something is null"
//                };
//            }
//        }

//        //public object ReadAllCommentByWcId(Guid wcid)
//        //{
//        //    db.Configuration.LazyLoadingEnabled = true;
//        //    db.Configuration.ProxyCreationEnabled = true;
//        //    var result = db.tbl_tbl_wc.Find(wcid).tbl_comment.ToArray();
//        //    db.Configuration.LazyLoadingEnabled = false;
//        //    db.Configuration.ProxyCreationEnabled = false;
//        //    return result;
//        //}

//        //public object ReadAllPhotosByWcid(Guid wcid)
//        //{
//        //    var wc = db.tbl_tbl_wc.FirstOrDefault(c => c.wc_id == wcid);
//        //    if (wc == null)
//        //    {
//        //        return new { none = "none" };
//        //    }
//        //    List<tbl_wcfile> result = new List<tbl_wcfile>();
//        //    return wc.tbl_wcfile.ToArray();
//        //}

  

//        //public object ReadWCDetailById(Guid wcid)
//        //{
//        //    db.Configuration.LazyLoadingEnabled = true;
//        //    db.Configuration.ProxyCreationEnabled = true;
//        //    var wc = db.tbl_tbl_wc.Find(wcid);
//        //    wc.tbl_comment = db.tbl_comment.Where(c => c.comment_wcid == wc.wc_id).ToList();
//        //    wc.tbl_wcfile = db.tbl_wcfile.Where(c => c.wcfile_wcid == wc.wc_id).ToList();
//        //    db.Configuration.LazyLoadingEnabled = false;
//        //    db.Configuration.ProxyCreationEnabled = false;
//        //    return wc;
//        //}

//        public object ReadWcNEAR(string latitude, string longtitude)
//        {
//            var wcs = db.tbl_tbl_wc.ToList();
//            double _latitude = 90;
//            Double.TryParse(latitude, out _latitude);
//            double _longtitude = 90;
//            Double.TryParse(longtitude, out _longtitude);
//            var topborder = _longtitude + 0.01f;
//            var bottomborder = _longtitude - 0.01f;
//            var rightborder = _latitude + 0.01f;
//            var leftborder = _latitude - 0.01f;

//            List<tbl_tbl_wc> Result = new List<tbl_tbl_wc>();
//            db.Configuration.LazyLoadingEnabled = true;
//            db.Configuration.ProxyCreationEnabled = true;
//            var rawData = wcs.Where(c => c.wc_latitude > bottomborder && c.wc_latitude < topborder && c.wc_longtitude < rightborder && c.wc_longtitude > leftborder).Take(10).ToArray();
//            db.Configuration.LazyLoadingEnabled = false;
//            db.Configuration.ProxyCreationEnabled = false;
//            return rawData; 
//        }

//        public object ReportContentByWC(Guid wcid)
//        {
//            db.tbl_tbl_wc.Find(wcid).wc_inappropriatereport++;
//            db.SaveChanges();
//            return new  { Status = GenerelTypeStatus.SUCCESS, Message = "Reported" };
//        } 

//        public object UpdateWC()
//        {
//            System.IO.StreamReader streamReader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
//            streamReader.BaseStream.Position = 0;
//            string requestFromPost = streamReader.ReadToEnd();
//            tbl_tbl_wc wc = JsonConvert.DeserializeObject<tbl_tbl_wc>(requestFromPost);

//            if (wc != null)
//            {

//                try
//                {

//                    var tbl_wc = db.tbl_tbl_wc.First(c => c.wc_id == wc.wc_id);
//                    var bufferlat = tbl_wc.wc_latitude;
//                    var bufferlon = tbl_wc.wc_longtitude;
//                    var buffernam = tbl_wc.wc_name;

//                    tbl_wc.wc_ablution = wc.wc_ablution;
//                    tbl_wc.wc_babystop = wc.wc_babystop;
//                    tbl_wc.wc_traditional = wc.wc_traditional;
//                    tbl_wc.wc_european = wc.wc_european;
//                    tbl_wc.wc_fordisabled = wc.wc_fordisabled;
//                    tbl_wc.wc_foreignid = wc.wc_foreignid.Replace("-", "").Trim();
//                    tbl_wc.wc_genderfemale = wc.wc_genderfemale;
//                    tbl_wc.wc_gendermale = wc.wc_gendermale;
//                    tbl_wc.wc_music = wc.wc_music;
//                    tbl_wc.wc_paper = wc.wc_paper;
//                    tbl_wc.wc_paid = wc.wc_paid;
//                    tbl_wc.wc_acpower = wc.wc_acpower;
//                    tbl_wc.wc_wifi = wc.wc_wifi;
//                    tbl_wc.wc_type = "5";
//                    tbl_wc.wc_name = buffernam;
//                    tbl_wc.wc_latitude = bufferlat;
//                    tbl_wc.wc_longtitude = bufferlon;

//                    db.Entry(tbl_wc).State = EntityState.Modified;
//                    db.SaveChanges();
//                   // Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(new Exception(new JavaScriptSerializer().Serialize(tbl_wc))));

//                    return new
//                    {
//                        Status = GenerelTypeStatus.SUCCESS,
//                        Message = ""
//                    };
//                }
//                catch (DbEntityValidationException e)
//                {
//                    string s = "";

//                    foreach (var eve in e.EntityValidationErrors)
//                    {
//                          s = string.Format("Entity türü \"{0}\" şu hatalara sahip \"{1}\" Geçerlilik hataları:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                        foreach (var ve in eve.ValidationErrors)
//                        {
//                            s = s + (string.Format("- Özellik: \"{0}\", Hata: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
//                        }
//                    }
//                    return new
//                    {
//                        Status = GenerelTypeStatus.FAILURE,
//                        Message = s
//                    };
//                }
//            }
//            else
//            {
//                return new 
//                {
//                    Status = GenerelTypeStatus.FAILURE,
//                    Message = "vallaha null geldi "
//                };
//            }
//        }

//        private string ReverseGeoCode(string Latitude, string Longtitude)
//        {
//            string address = string.Empty;
//            try
//            {
//                string requestURL = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + Latitude.Replace(",", ".") + "," + Longtitude.Replace(",", ".") + "&key=AIzaSyBeY9goI6qImdZJ8_Wtd6sJQEneRf4ET1Q";
//                string responseString = gethttpPage(requestURL);
//                dynamic data = System.Web.Helpers.Json.Decode(responseString);
//                if (data.status == "OK")
//                {
//                    for (int i = 0; i < data.results[0].address_components.Length; i++)
//                    {
//                        if (data.results[0].address_components[i].types[0] != "postal_code" && data.results[0].address_components[i].types[0] != "country" && data.results[0].address_components[i].types[0] != "administrative_area_level_1")
//                        {
//                            address = data.results[0].address_components[i].long_name + ", " + address;
//                        }
//                    }
//                    if (address.LastIndexOf(',') != -1)
//                    {
//                        address.Remove(address.LastIndexOf(','), 1).TrimEnd();
//                    }
//                }
//                else
//                {
//                    address = "İsim Bulunamadı.";
//                }
//            }
//            catch (Exception)
//            {
//                address = "İsim Bulunamadı.";
//            }
//            return address;
//        }

//        private static string gethttpPage(string adres)
//        {
//            StreamReader sr;
//            String tex2;
//            try
//            {
//                HttpWebRequest istek = (HttpWebRequest)HttpWebRequest.Create(adres);
//                HttpWebResponse cevap = (HttpWebResponse)istek.GetResponse();
//                sr = new StreamReader(cevap.GetResponseStream(), Encoding.UTF8);
//            }
//            catch (WebException e)
//            {
//                return "";
//            }
//            return tex2 = sr.ReadToEnd();
//        }

//        //public object Authenticate()
//        //{
//        //    System.IO.StreamReader streamReader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
//        //    streamReader.BaseStream.Position = 0;
//        //    string requestFromPost = streamReader.ReadToEnd();
//        //    AuthenticationRequestType authenticationrequesttype = JsonConvert.DeserializeObject<AuthenticationRequestType>(requestFromPost);


//        //    var user = db.tbl_user.FirstOrDefault(c => c.user_id == authenticationrequesttype.user_id);
//        //    if (user != null)
//        //    {
//        //        if (user.user_banned)
//        //        {
//        //            return new  { Status = GenerelTypeStatus.OK, Message = "9581445f187bbab2185e1fae2bb3c8c7" };
//        //        }
//        //        else
//        //        {
//        //            var result = new  { Status = GenerelTypeStatus.OK, Message = AuthSessionKey(authenticationrequesttype.user_sessionKey).ToLower() };
//        //            return result;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        string surname = "";
//        //        string name = "";
//        //        if (authenticationrequesttype.user_name.Contains(" "))
//        //        {
//        //            surname = authenticationrequesttype.user_name.Split(' ').ToList().Last();
//        //            name = authenticationrequesttype.user_name.Split(' ')[0];
//        //            if (authenticationrequesttype.user_name.Split(' ').Length > 2)
//        //            {
//        //                name = name + " " + authenticationrequesttype.user_name.Split(' ')[1];
//        //            }
//        //        }
//        //        db.tbl_user.Add(new tbl_user
//        //        {
//        //            user_id = authenticationrequesttype.user_id,
//        //            user_deviceId = authenticationrequesttype.user_deviceId,
//        //            user_macAddress = authenticationrequesttype.user_macAddress,
//        //            user_banned = false,
//        //            user_mail = authenticationrequesttype.user_mail,
//        //            user_name = name,
//        //            user_photourl = "",
//        //            user_surname = surname,
//        //            user_token = authenticationrequesttype.user_token
//        //        });
//        //        db.SaveChanges();
//        //        var result = new { Status = GenerelTypeStatus.OK, Message = AuthSessionKey(authenticationrequesttype.user_sessionKey) };
//        //        return result;
//        //    }
//        //}

//        private string AuthSessionKey(string sessionkey)
//        {
//            DateTime realtime = GetNetworkTime();
//            //var timeStamp = realtime.Year.ToString()+ realtime.Month.ToString() + realtime.Day.ToString() + (realtime.AddHours(3)).Hour.ToString()   + ""; 
//            var timeStamp = (realtime.AddHours(3)).Hour.ToString() + (realtime.Minute).ToString() + "";
//            return CalculateMD5Hash(sessionkey + timeStamp + "90278654-f670-400c-8c19-16fc82821e55");
//        }

//        private string CalculateMD5Hash(string input)
//        {
//            MD5 md5 = System.Security.Cryptography.MD5.Create();
//            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
//            byte[] hash = md5.ComputeHash(inputBytes);
//            StringBuilder sb = new StringBuilder();
//            for (int i = 0; i < hash.Length; i++)
//            {
//                sb.Append(hash[i].ToString("X2"));
//            }
//            return sb.ToString();
//        }

//        private DateTime GetNetworkTime()
//        {
//            const string ntpServer = "pool.ntp.org";
//            var ntpData = new byte[48];
//            ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

//            var addresses = Dns.GetHostEntry(ntpServer).AddressList;
//            var ipEndPoint = new IPEndPoint(addresses[0], 123);
//            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//            socket.Connect(ipEndPoint);
//            socket.Send(ntpData);
//            socket.Receive(ntpData);
//            socket.Close();

//            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
//            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

//            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
//            var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

//            return networkDateTime;
//        }
//        private Y ConvertDataReaderToOrmObject<Y>(SqlDataReader t, Y y)
//        {
//            var info = typeof(Y).GetProperties();
//            var DestClass = typeof(Y);

//            foreach (var property in info)
//            {
//                var localpropertyname = property.Name;
//                try
//                {
//                    var localpropertyvalue = t[localpropertyname];
//                    DestClass.GetProperty(localpropertyname).SetValue(y, localpropertyvalue);
//                }
//                catch (Exception e)
//                {
//                    //   Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(new Exception("Sıkıntılı>>>>:" + localpropertyname)));
//                }
//            }
//            return y;
//        }
//    }
//}
