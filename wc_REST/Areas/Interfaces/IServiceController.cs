using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wc_REST.Areas.Interfaces
{
    public interface IServiceController
    {
        #region GeneralOperations  

        object InsertNewWC(); 
        object UpdateWC(); 
        object AddNewComment(Guid wcid, Guid userid, string comment); 
        object AddNewPhoto(Guid wcid, Guid userid); 
        object GiveRate(Guid wcid, Guid userid, int score); 
        object ReadAllPhotosByWcid(Guid wcid); 
        object ReadAllCommentByWcId(Guid wcid); 
        object ReadWCDetailById(Guid wcid); 
        object ReportContentByWC(Guid wcid);
        #endregion

        #region  commentOperation 
        //object ReadAllCommentByUserId(Guid User_Id);
        #endregion 

        #region  WcOperation 
        object ReadWcNEAR(string latitude,string longtitude);  
        object Authenticate();
        #endregion

        // TODO: Add your service operations here
    }
}