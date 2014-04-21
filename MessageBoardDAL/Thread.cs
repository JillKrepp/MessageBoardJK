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
    public class Thread
    {
        public int forum_id { get; set; }
        public int thread_id { get; set; }
        public int view_count { get; set; }
        public Post OpeningPost { get; set; }

        public static List<Thread> GetThreadsByForumId(int forum_id)
        {
            DataTable table = ForumDB.QuerySproc("GetThreadsByForumId", new SqlParameter("forum_id", forum_id));
            List<Thread> results = new List<Thread>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Thread result = MapRow(table.Rows[i]);
                results.Add(result);
            }
            return results;
        }

        private static Thread MapRow(DataRow row)
        {
            Thread result = new Thread
            {
                forum_id = Convert.ToInt32(row["forum_id"]),
                thread_id = Convert.ToInt32(row["thread_id"]),
                OpeningPost = new Post
                {
                    subject = Convert.ToString(row["subject"]),
                    content = Convert.ToString(row["content"]),
                    post_date = Convert.ToDateTime(row["post_date"]),
                    post_id = Convert.ToInt32(row["post_id"]),
                    reply_to = null,
                    user =
                        new User
                        {
                            UserId = Convert.ToInt32(row["user_id"]),
                            Username = Convert.ToString(row["username"])
                        }
                }
              

            };
            return result;
        }

        public void Save()
        {
            int thread_id = (int)ForumDB.ExecuteScalar("AddThread", new SqlParameter("forum_id", this.forum_id), new SqlParameter("subject", this.OpeningPost.subject), new SqlParameter("content", this.OpeningPost.content), new SqlParameter("user_id", this.OpeningPost.user.UserId));
            this.thread_id = thread_id;
            
        }

        public void AddPost(Post post)
        {
            int post_id = (int)ForumDB.ExecuteScalar("ReplyToPost", new SqlParameter("thread_id", this.thread_id), new SqlParameter("user_id", post.user.UserId), new SqlParameter("content", post.content), new SqlParameter("reply_to", post.reply_to));
            post.post_id = post_id;
        }

        public static Thread GetThreadByThreadId(int thread_id)
        {
            DataTable table = ForumDB.QuerySproc("GetThreadByThreadId", new SqlParameter("thread_id", thread_id));
            
            if (table.Rows.Count == 1)
            {
                return MapRow(table.Rows[0]);             
            }
            return null;

        }
                
    }
}
