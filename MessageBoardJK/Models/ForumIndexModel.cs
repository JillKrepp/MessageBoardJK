using MessageBoardDAL;
using System.Collections.Generic;

namespace MessageBoardJK.Models
{
    public class ForumIndexModel : BaseModel
    {
        public List<Forum> Forums { get; set; }
    }
}