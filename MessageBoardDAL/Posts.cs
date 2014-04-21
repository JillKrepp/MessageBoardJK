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
    public class Post
    {
        public int post_id { get; set; }
        public User user { get; set; }
        public int thread_id { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public DateTime post_date { get; set; }
        public int? reply_to { get; set; }

        public static List<Post> GetPostsByThreadId(int thread_id)
        {
            DataTable table = ForumDB.QuerySproc("GetPostsByThreadId", new SqlParameter("thread_id", thread_id));
            List<Post> results = new List<Post>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Post result = new Post
                {

                    post_id = table.Rows[i].ToInt32("post_id"),
                    thread_id = table.Rows[i].ToInt32("thread_id"),
                    subject = table.Rows[i].ToString("subject"),
                    content = table.Rows[i].ToString("content"),
                    post_date = table.Rows[i].ToDateTime("post_date"),
                    reply_to = table.Rows[i].ToNullableInt32("reply_to"),
                    user = new User
                    {
                        UserId = table.Rows[i].ToInt32("user_id"),
                        Username = table.Rows[i].ToString("username")
                    }

                };
                results.Add(result);
            }
            return results;
        }
    }
}
