using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoardDAL;

namespace MessageBoardJK.Models
{
    public class NewPostModel : BaseModel
    {
        public string PostBackAction { get; set; }
        public Thread Thread { get; set; }
        public Post Post { get; set; }
        public Forum Forum { get; set;}
        
    }
}