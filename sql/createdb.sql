
/****** Object:  StoredProcedure [dbo].[AddThread]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddThread]
	(
		@forum_id varchar(20),
		@subject varchar(255),
		@content varchar(max),
		@user_id int	
	)
AS
BEGIN
	
	Declare @output Table (thread_id int)
	declare @thread_id int
	
	Insert into threads (forum_id, viewcount)
	Output inserted.thread_id into @output(thread_id)
	Values (@forum_id, 1)

	select @thread_id = thread_id from @output
	Insert into posts ( thread_id, subject, content, post_date, user_id)
	Output inserted.thread_id
	Values (@thread_id, @subject, @content, GETDATE(), @user_id)


	
END

GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddUser]
	(
		@username varchar(20), 
		@password varchar(128)
		)
	
AS
BEGIN
	INSERT INTO users (username, password)
	OUTPUT inserted.user_id
	VALUES (@username,@password)

	 
END

GO
/****** Object:  StoredProcedure [dbo].[GetForumByForumID]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetForumByForumID]
	(@forum_id int)
AS
BEGIN
	SELECT forum_id, name
	FROM forums
	Where forum_id = @forum_id 
END

GO
/****** Object:  StoredProcedure [dbo].[GetForumIndex]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetForumIndex]
AS
BEGIN
	SELECT forum_id, name
	FROM forums
END
GO
/****** Object:  StoredProcedure [dbo].[GetPostsByThreadId]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPostsByThreadId] @thread_Id int
AS
BEGIN
	Select
	posts.post_id,
	posts.thread_id,
	posts.subject,
	posts.content,
	posts.post_date,
	posts.reply_to,
	users.username,
	users.user_id
	from posts
	inner join users on 
		posts.user_id = users.user_id
	where
		thread_id = @thread_Id
	
END

GO
/****** Object:  StoredProcedure [dbo].[GetThreadByThreadId]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetThreadByThreadId]
	(
	@thread_id int
	)

AS
BEGIN
	Select
		post_id, 
		users.user_id,
		subject, 
		content,
		post_date,
		reply_to, 
		posts.thread_id, 
		threads.forum_id, 
		threads.viewcount, 
		users.username
	From posts
	inner join threads on 
		posts.thread_id=threads.thread_id
	inner join users on
		users.user_id=posts.user_id
	where threads.thread_id = @thread_id and reply_to is null
END

GO
/****** Object:  StoredProcedure [dbo].[GetThreadsByForumID]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetThreadsByForumID] @forum_id int
AS
BEGIN
	Select
	threads.forum_id,
	threads.thread_id,
	posts.post_date,
	posts.post_id,
	posts.user_id,
	posts.subject,
	posts.content,
	users.username
From threads
inner join posts on
	threads.thread_id=posts.thread_id
inner join users on
	users.user_id=posts.user_id
where
	forum_id = @forum_id and reply_to is null

	
END

GO
/****** Object:  StoredProcedure [dbo].[GetUserByUsernameAndPassword]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByUsernameAndPassword]
	-- Add the parameters for the stored procedure here
	@username varchar(20), @password varchar(128)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT user_id, username, password
	from users 
	where username = @username and password = @password
END

GO
/****** Object:  StoredProcedure [dbo].[ReplyToPost]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ReplyToPost]
(
	@thread_id int,
	@user_id int,
	@content varchar(max),
	@reply_to int
)
AS
BEGIN
	Insert into posts (thread_id, user_id, post_date, content, reply_to)
	Output inserted.post_id
	Values (@thread_id, @user_id, GETDATE(), @content, @reply_to)
END

GO
/****** Object:  StoredProcedure [dbo].[report_GetForumSummaryByDate]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[report_GetForumSummaryByDate]
	@startDate datetime,
	@endDate datetime
AS
BEGIN
	select
		Forum.Name,
		ThreadCount = count(distinct thread.thread_id),
		PostCount = count(post.post_id)
	from forums forum
	inner join threads thread on
		thread.forum_id = forum.forum_id
	inner join posts post on
		post.thread_id = thread.thread_id
	where
		post.post_date between @startDate and @endDate
	group by
		forum.name
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
			(
				@password varchar(128),
				@user_id int
			)
AS
BEGIN
UPDATE users
set password = @password
where user_id=@user_id
END

GO
/****** Object:  Table [dbo].[forums]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[forums](
	[forum_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
 CONSTRAINT [PK__FORUMS__69A2FA58A2B17F7A] PRIMARY KEY CLUSTERED 
(
	[forum_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ignore]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ignore](
	[user_id] [int] NOT NULL,
	[ignore_user_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[posts]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[posts](
	[post_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[thread_id] [int] NOT NULL,
	[subject] [varchar](255) NULL,
	[content] [varchar](max) NOT NULL,
	[post_date] [datetime] NOT NULL,
	[reply_to] [int] NULL,
 CONSTRAINT [PK__POSTS__3ED787660608518B] PRIMARY KEY CLUSTERED 
(
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[threads]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[threads](
	[thread_id] [int] IDENTITY(1,1) NOT NULL,
	[viewcount] [int] NOT NULL,
	[forum_id] [int] NOT NULL,
 CONSTRAINT [PK__THREADS__7411E2F0058339ED] PRIMARY KEY CLUSTERED 
(
	[thread_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 4/20/2014 9:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[password] [varchar](128) NOT NULL,
 CONSTRAINT [PK__USERS__B9BE370F4B52D286] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_users] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ignore]  WITH CHECK ADD  CONSTRAINT [FK_ignore_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[ignore] CHECK CONSTRAINT [FK_ignore_users]
GO
ALTER TABLE [dbo].[ignore]  WITH CHECK ADD  CONSTRAINT [FK_ignore_users1] FOREIGN KEY([ignore_user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[ignore] CHECK CONSTRAINT [FK_ignore_users1]
GO
ALTER TABLE [dbo].[posts]  WITH CHECK ADD  CONSTRAINT [FK__POSTS__user_id__38996AB5] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[posts] CHECK CONSTRAINT [FK__POSTS__user_id__38996AB5]
GO
ALTER TABLE [dbo].[posts]  WITH CHECK ADD  CONSTRAINT [FK_posts_posts1] FOREIGN KEY([reply_to])
REFERENCES [dbo].[posts] ([post_id])
GO
ALTER TABLE [dbo].[posts] CHECK CONSTRAINT [FK_posts_posts1]
GO
ALTER TABLE [dbo].[posts]  WITH CHECK ADD  CONSTRAINT [FK_posts_threads] FOREIGN KEY([thread_id])
REFERENCES [dbo].[threads] ([thread_id])
GO
ALTER TABLE [dbo].[posts] CHECK CONSTRAINT [FK_posts_threads]
GO
ALTER TABLE [dbo].[threads]  WITH CHECK ADD  CONSTRAINT [FK__THREADS__forum_i__3D5E1FD2] FOREIGN KEY([forum_id])
REFERENCES [dbo].[forums] ([forum_id])
GO
ALTER TABLE [dbo].[threads] CHECK CONSTRAINT [FK__THREADS__forum_i__3D5E1FD2]
GO
USE [master]
GO
ALTER DATABASE [ForumDB] SET  READ_WRITE 
GO
