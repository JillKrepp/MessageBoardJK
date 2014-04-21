using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardDAL
{
    public class Forum
    {
        public int forum_id;
        public string name;


        public static List<Forum> GetForumIndex()
        {
            DataTable table = ForumDB.QuerySproc("GetForumIndex");
            List<Forum> results = new List<Forum>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Forum result = new Forum
                {

                    forum_id = Convert.ToInt32(table.Rows[i]["forum_id"]),
                    name = Convert.ToString(table.Rows[i]["name"])
                };
                results.Add(result);
            }
            return results;
        }
        public static Forum GetForumByForumId(int forum_id)
        {      
            DataTable table = ForumDB.QuerySproc("GetForumByForumId", new SqlParameter("forum_id", forum_id));
            if (table.Rows.Count == 1)
            {
                return
                    new Forum
                    {
                        forum_id = Convert.ToInt32(table.Rows[0]["forum_id"]),
                        name = Convert.ToString(table.Rows[0]["name"])
                    };
            }
            return null;
        }

    }
}
