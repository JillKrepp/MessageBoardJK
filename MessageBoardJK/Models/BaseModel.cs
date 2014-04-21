using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoardDAL;
using MessageBoardJK.Controllers;

namespace MessageBoardJK.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            BreadCrumb = new BreadCrumb();
        }

        public User CurrentUser
        {
            get { return SessionContext.CurrentUser; }
        }

        public BreadCrumb BreadCrumb { get; set; }
    }
}