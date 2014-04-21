using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoardDAL;

namespace MessageBoardJK.Models
{
    public class ViewPostsModel : BaseModel
    {
        public List<Post> posts { get; set; }
        public Thread thread { get; set; }
        public Forum forum { get; set; }
    }
}