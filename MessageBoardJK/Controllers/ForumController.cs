using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MessageBoardDAL;
using MessageBoardJK.Models;
using ForumUser = MessageBoardDAL.User;

namespace MessageBoardJK.Controllers
{
    public class ForumController : Controller
    {
        //
        // GET: /Home/    
        public ActionResult Index()
        {
            ForumIndexModel model = new ForumIndexModel();
            model.Forums = Forum.GetForumIndex();

            return View("Index", model);
        }
        public ActionResult Register()
        {
            BaseModel model = new BaseModel();
            model.BreadCrumb.HeaderText = "New User Registration";
            return View("NewUser", model);
        }
        public ActionResult CreateUser(ForumUser user)
        {
            user.Save();
            SessionContext.CurrentUser = user;
            return Index();
           
        }
        public ActionResult Login(string username, string password)
        {
            var user = MessageBoardDAL.User.GetUserByUsernameAndPassword(username, password);
            SessionContext.CurrentUser = user;

            return Index();
        }

        public ActionResult ViewForum(int id)
        {
            ViewForumModel model = new ViewForumModel();
            model.Threads = Thread.GetThreadsByForumId(id);
            model.Forum = Forum.GetForumByForumId(id);
            model.BreadCrumb.Forum = model.Forum;
            return View(model);
        }
        public ActionResult ViewThread(int id)
        {
            ViewPostsModel model = new ViewPostsModel();
            model.thread = Thread.GetThreadByThreadId(id);
            model.posts = Post.GetPostsByThreadId(id);
            model.forum = Forum.GetForumByForumId(model.thread.forum_id);
            model.BreadCrumb.Thread = model.thread;
            model.BreadCrumb.Forum = model.forum;
            return View("ViewThread", model);
        }
        
        public ActionResult NewThread(int forum_id)
        {
            if (SessionContext.CurrentUser == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                NewPostModel model = new NewPostModel();
                model.PostBackAction = "CreateThread";
                model.Thread = new Thread { forum_id = forum_id };
                model.Forum = Forum.GetForumByForumId(forum_id);
                model.BreadCrumb.HeaderText = "New Thread";
                model.BreadCrumb.Thread = model.Thread;
                model.BreadCrumb.Forum = model.Forum;
                return View("NewPost", model);
            }
        }
        public ActionResult CreateThread(NewPostModel model)
        {
            Thread thread = model.Thread;
            thread.OpeningPost = model.Post;
            thread.OpeningPost.user = model.CurrentUser;
            thread.Save();
           
            return RedirectToAction("ViewThread", new { id = thread.thread_id });
        }
        public ActionResult NewReply(int thread_id)
        {
            if (SessionContext.CurrentUser == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
             
                NewPostModel model = new NewPostModel();
                model.PostBackAction = "CreateReply";
                model.Post = new Post();
                model.Thread = Thread.GetThreadByThreadId(thread_id);
                model.Forum = Forum.GetForumByForumId(model.Thread.forum_id);
                model.Post.reply_to = model.Thread.OpeningPost.post_id;
                model.BreadCrumb.HeaderText = "New Post";
                model.BreadCrumb.Thread = model.Thread;
                model.BreadCrumb.Forum = model.Forum;
                return View("NewPost", model);
            }
        }
        public ActionResult CreateReply(NewPostModel model)
        {
            Thread thread = model.Thread;
            Post post = model.Post;
            post.user = model.CurrentUser;
            thread.AddPost(post);

            return RedirectToAction("ViewThread", new { id = thread.thread_id });
        }
       
    }
}
