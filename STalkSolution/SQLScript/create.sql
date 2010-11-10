SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tb_User]') AND type in (N'U'))
BEGIN
CREATE TABLE [Tb_User](
	[UserID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](15) NOT NULL,
	[UserPwd] [nvarchar](32) NULL,
	[RegTime] [datetime] NULL CONSTRAINT [DF_Tb_User_RegTime]  DEFAULT (getdate()),
	[LastLoginIP] [nvarchar](20) NULL CONSTRAINT [DF_Tb_User_LastLoginIP]  DEFAULT (''),
	[LastLoginTime] [datetime] NULL CONSTRAINT [DF_Tb_User_LastLoginTime]  DEFAULT (getdate()),
	[Status] [tinyint] NULL CONSTRAINT [DF_Tb_User_Status]  DEFAULT ((0)),
	[Server] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Tb_User]') AND name = N'IX_Tb_User')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tb_User] ON [Tb_User] 
(
	[UserName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 正常 1 锁定' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Tb_User', @level2type=N'COLUMN', @level2name=N'Status'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tb_UserInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [Tb_UserInfo](
	[UserID] [bigint] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[NickName] [nvarchar](20) NULL,
 CONSTRAINT [PK_Tb_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
