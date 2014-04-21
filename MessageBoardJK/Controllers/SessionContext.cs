using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoardDAL;

namespace MessageBoardJK.Controllers
{
    public static class SessionContext
    {
        public static User CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["CurrentUser"] as User;
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }
    }
}