using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoardDAL;

namespace MessageBoardJK.Models
{
    public class BreadCrumb
    {
        public Forum Forum { get; set; }
        public Thread Thread { get; set; }
        public string HeaderText { get; set; }
    }
}