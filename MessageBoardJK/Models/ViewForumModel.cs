using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoardDAL;

namespace MessageBoardJK.Models
{
    public class ViewForumModel : BaseModel
    {
        public List<Thread> Threads { get; set;  }
        public Forum Forum { get; set; }
    }
    


}