-- ======================================================================
-- 创建LTMvc 具体业务表
-- 创建人：秦超
-- 创建时间：2016/10/28
-- ======================================================================



-- ======================================================================
-- 网站新闻信息的管理
-- ======================================================================
USE LTMvc 
GO

---新闻信息条目表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_News')
DROP TABLE T_News
GO
CREATE TABLE T_News
(
	[id]                [nvarchar](200) PRIMARY KEY NOT null,
	[news_title]        [nvarchar](100)     NOT NULL,
	[catalog_name]      [nvarchar](200),
	[catalog_id]        [nvarchar](200),
	[news_summary]      [nvarchar](400),
	[is_published]      [bit] DEFAULT 0     NOT NULL,
	[publish_date]      [datetime],
	[write_date]        [datetime],
	[view_times]        [int],
	[is_del]            [bit] DEFAULT 0     NOT NULL,

	[create_time]       [datetime]          NOT NULL,
	[creater_name]      [nvarchar](50)      NOT NULL,
	[updated_time]      [datetime]          NOT NULL,
	[updater_name]      [nvarchar](50)      NOT NULL,
	[record_order]      [int] IDENTITY(1,1) NOT NULL
)
GO

--新闻详细信息条目表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_NewsInfo')
DROP TABLE T_NewsInfo
CREATE TABLE T_NewsInfo
(
	[id]                [nvarchar](200) PRIMARY KEY NOT NULL,
	[news_id]           [nvarchar](200) 	NOT NULL,
	[news_info]         [text],
	[is_del]            [bit] DEFAULT 0     NOT NULL,

	[create_time]       [datetime]          NOT NULL,
	[creater_name]      [nvarchar](50)      NOT NULL,
	[updated_time]      [datetime]          NOT NULL,
	[updater_name]      [nvarchar](50)      NOT NULL,
	[record_order]      [int] IDENTITY(1,1) NOT NULL
)
GO

--创建文档文件夹信息表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_Catalog')
DROP TABLE T_Catalog
CREATE TABLE T_Catalog
(
	[id]                [nvarchar](200)  PRIMARY KEY NOT NULL,
	[catalog_name]      [nvarchar](200),
	[catalog_pid]       [nvarchar](200),

	[create_time]       [datetime]          NOT NULL,
	[creater_name]      [nvarchar](50)      NOT NULL,
	[updated_time]      [datetime]          NOT NULL,
	[updater_name]      [nvarchar](50)      NOT NULL,
	[record_order]      [int] IDENTITY(1,1) NOT NULL
)

--^_^
INSERT INTO T_Catalog(id, catalog_name, catalog_pid, create_time, creater_name, updated_time, updater_name)
VALUES (REPLACE(NEWID(),(''),('')), '小新闻', '0512EE5D-88A0-4FD9-915B-04CA445D2818', GETDATE(), '管理员', GETDATE(), '管理员');