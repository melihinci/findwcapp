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
    public class SearchWcByParamsController : ApiController
    {

        //public object Get(string Parameters, string Latitude, string Longitude, int Rate)
        //{
        //    string[] Conditions;
        //    var longtitude = double.Parse(Latitude, CultureInfo.InvariantCulture);
        //    var latitude = double.Parse(Longitude, CultureInfo.InvariantCulture);
        //    decimal rate = Rate / 100;



        //    #region QUERY_CONSTRUCT 
        //    var topborder = latitude + 0.01f;
        //    var bottomborder = latitude - 0.01f;
        //    var rightborder = longtitude + 0.01f;
        //    var leftborder = longtitude - 0.01f;
        //    string query = "SELECT * FROM tbl_wc Where  wc_longtitude > " + bottomborder.ToString().Replace(",", ".") + " AND  wc_longtitude < " + topborder.ToString().Replace(",", ".") + " AND  wc_latitude > " + leftborder.ToString().Replace(",", ".") + " AND  wc_latitude < " + rightborder.ToString().Replace(",", ".") + " AND ";
        //    //string requestlog = "INSERT INTO tbl_request(Request_Id,Request_date,Request_wc_latitude,Request_wc_longtitude,";
        //    int propertycount = 0;
        //    if (Parameters != null)
        //    {
        //        Conditions = Parameters.Split('|');
        //        //var info = typeof(tbl_wc).GetProperties(); 
        //        foreach (var property in Conditions)
        //        { 
        //            //    var localpropertyname = property.Name;
        //            //    var localvalure = property.GetValue(wc, null);
        //            try
        //            {
        //                propertycount++;
        //                query = query + " " + property + "=1 AND ";
        //                ////requestlog = requestlog + " " + "Request_" + property + ","; 
        //            }
        //            catch (Exception e)
        //            {
        //                throw;
        //            }
        //        }
        //    }

        //    ////requestlog = requestlog.Substring(0, requestlog.Length - 1);
        //    ////requestlog = requestlog + ") VALUES('" + Guid.NewGuid() + "','" + DateTime.Now + "'," + latitude.ToString().Replace(",", ".") + "," + longtitude.ToString().Replace(",", ".") + ",";
        //    //// for (int i = 0; i < propertycount; i++)
        //    ////{
        //    ////    requestlog = requestlog + "'1',";
        //    ////}                                                    
        //    query = query.Substring(0, query.LastIndexOf("AND "));//!\

        //    //requestlog = requestlog.Substring(0, requestlog.Length - 1);
        //    //requestlog = requestlog + ")";

        //    #endregion
        //    #region OPERATIONS
        //    //List<tbl_tbl_wc> result = new List<tbl_tbl_wc>();
        //    //NEREYEAPPEntities2 db = new NEREYEAPPEntities2();
        //    //db.Database.Connection.Close();
        //    //using (SqlConnection connect = new SqlConnection(
        //    //    "Password=findWC123??;Persist Security Info=True;User ID=sa;Initial Catalog=NEREYEAPP;Data Source=136.243.174.241,1433"))
        //    //{
        //    //    SqlCommand command = new SqlCommand(query);
        //    //    command.Connection = connect;
        //    //    connect.Open();
        //    //    using (SqlDataReader reader = command.ExecuteReader())
        //    //    {
        //    //        if (reader.HasRows)
        //    //        {
        //    //            while (reader.Read())
        //    //            {
        //    //                result.Add(ConvertDataReaderToOrmObject(reader, new tbl_tbl_wc()));
        //    //            }
        //    //        }
        //    //    }
        //    //    //new SqlCommand(requestlog, connect).ExecuteNonQuery();
        //    //    connect.Close();
        //    //}
        //    #endregion
        //    db.Database.Connection.Open();

        //    db.Configuration.LazyLoadingEnabled = true;
        //    db.Configuration.ProxyCreationEnabled = true;
        //    if (result.Count == 0) return new { none = "none" };
        //    return (result.Take(25)).ToArray();
        //}

        private string ReverseGeoCode(string Latitude, string Longtitude)
        {
            string address = string.Empty;
            try
            {
                string requestURL = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + Latitude.Replace(",", ".") + "," + Longtitude.Replace(",", ".") + "&key=AIzaSyBeY9goI6qImdZJ8_Wtd6sJQEneRf4ET1Q";
                string responseString = gethttpPage(requestURL);
                dynamic data = System.Web.Helpers.Json.Decode(responseString);
                if (data.status == "OK")
                {
                    for (int i = 0; i < data.results[0].address_components.Length; i++)
                    {
                        if (data.results[0].address_components[i].types[0] != "postal_code" && data.results[0].address_components[i].types[0] != "country" && data.results[0].address_components[i].types[0] != "administrative_area_level_1")
                        {
                            address = data.results[0].address_components[i].long_name + ", " + address;
                        }
                    }
                    if (address.LastIndexOf(',') != -1)
                    {
                        address.Remove(address.LastIndexOf(','), 1).TrimEnd();
                    }
                }
                else
                {
                    address = "İsim Bulunamadı.";
                }
            }
            catch (Exception)
            {
                address = "İsim Bulunamadı.";
            }
            return address;
        }

        private static string gethttpPage(string adres)
        {
            StreamReader sr;
            String tex2;
            try
            {
                HttpWebRequest istek = (HttpWebRequest)HttpWebRequest.Create(adres);
                HttpWebResponse cevap = (HttpWebResponse)istek.GetResponse();
                sr = new StreamReader(cevap.GetResponseStream(), Encoding.UTF8);
            }
            catch (WebException e)
            {
                return "";
            }
            return tex2 = sr.ReadToEnd();
        }



        private Y ConvertDataReaderToOrmObject<Y>(SqlDataReader t, Y y)
        {
            var info = typeof(Y).GetProperties();
            var DestClass = typeof(Y);

            foreach (var property in info)
            {
                var localpropertyname = property.Name;
                try
                {
                    var localpropertyvalue = t[localpropertyname];
                    DestClass.GetProperty(localpropertyname).SetValue(y, localpropertyvalue);
                }
                catch (Exception e)
                {
                    //   Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(new Exception("Sıkıntılı>>>>:" + localpropertyname)));
                }
            }
            return y;
        }
    }
}