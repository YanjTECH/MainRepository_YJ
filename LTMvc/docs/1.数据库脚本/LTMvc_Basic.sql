-- ======================================================================
-- 创建LTMvc数据库和基础数据表
-- 创建人：秦超
-- 创建时间：2016/10/20
-- =======================================================================

--指向当前要使用的数据库
USE [master]
GO
IF EXISTS (SELECT * FROM sysdatabases WHERE name = 'LTMvc')
DROP DATABASE LTMvc
--创建数据库
CREATE DATABASE LTMvc
ON PRIMARY
(
	--数据库文件的逻辑名
	NAME = 'LTMvc_data',
	--数据库物理文件名（绝对路径）
	FILENAME = 'C:\DB\LTMvc_data.mdf',
	--数据库文件初始大小
	SIZE = 10MB,
	--数据库文件增长量
	FILEGROWTH = 1MB
)

--创建日志文件
LOG ON
(
	NAME = 'LTMvc_log',
	FILENAME = 'C:\DB\LTMvc_log.ldf',
	SIZE = 2MB,
	FILEGROWTH = 2MB
)


-- ======================================================================
-- 创建基础数据表
-- 每张表都包含以下字段
-- id:主键（数据类型：nvarchar；存放一个GUID数据）
-- create_time: （datetime,创建时间）在SQL Server 下设置默认值为GETDATE()
-- creater_name:（navarchar,创建人）保存创建记录的用户名
-- updated_time:（datetime,更新时间）最近一次的更新时间
-- updater_name:（nvachar，更新人）保存最近更新记录的用户名
-- record_order:（int，记录排序）
-- =======================================================================

--创建数据库表信息表
USE LTMvc
GO
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_TableInfo')
DROP TABLE T_TableInfo
GO
CREATE TABLE T_TableInfo
(
	[id]                [nvarchar](200) PRIMARY KEY  NOT NULL,
	[table_name]        [nvarchar](200)     NOT NULL,
	[table_notes]       [nvarchar](max)     NOT NULL,
	[is_del]            [bit] DEFAULT 0     NOT NULL,
	[create_time]       [datetime]          NOT NULL,
	[creater_name]      [nvarchar](50)      NOT NULL,
	[updated_time]      [datetime]          NOT NULL,
	[updater_name]      [nvarchar](50)      NOT NULL,
	[record_order]      [int] IDENTITY(1,1) NOT NULL
)
GO

--创建数据库表字段信息表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_FieldInfo')
DROP TABLE T_FieldInfo
GO
CREATE TABLE T_FieldInfo
(
	[id]                [nvarchar](200) PRIMARY KEY  NOT NULL,
	[field_name]        [nvarchar](200)     NOT NULL,
	[field_type]        [nvarchar](200)     NOT NULL,
	[is_null]           [bit] DEFAULT 0     NOT NULL,
	[is_del]            [bit] DEFAULT 0     NOT NULL,
  	[table_name]        [nvarchar](200)     NOT NULL,
	[field_notes]       [nvarchar](max)     NOT NULL,
	[create_time]       [datetime]          NOT NULL,
	[creater_name]      [nvarchar](50)      NOT NULL,
	[updated_time]      [datetime]          NOT NULL,
	[updater_name]      [nvarchar](50)      NOT NULL,
	[record_order]      [int] IDENTITY(1,1) NOT NULL
)
GO

--创建视图信息表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_ViewInfo')
DROP TABLE T_ViewInfo
GO
CREATE TABLE T_ViewInfo
(
	[id]               [nvarchar](200)   PRIMARY KEY   NOT NULL,
	[view_name]        [nvarchar](200)     NOT NULL,
	[view_notes]       [nvarchar](max)     NOT NULL,
	[is_del]           [bit] DEFAULT 0     NOT NULL,         
	[create_time]      [datetime]          NOT NULL,
	[creater_name]     [nvarchar](50)      NOT NULL,
	[updated_time]     [datetime]          NOT NULL,
	[updater_name]     [nvarchar](50)      NOT NULL,
	[record_order]     [int] IDENTITY(1,1) NOT NULL
)
GO

--创建用户操作日志表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_LogInfo')
DROP TABLE T_LogInfo
GO
CREATE TABLE T_LogInfo
(
	[id]               [nvarchar](200)  PRIMARY KEY   NOT NULL,
	[operater_name]    [nvarchar](200)     NOT NULL,
	[log_description]  [nvarchar](max)     NOT NULL,
	[create_time]      [datetime]          NOT NULL,
	[creater_name]     [nvarchar](50)      NOT NULL,
	[updated_time]     [datetime]          NOT NULL,
	[updater_name]     [nvarchar](50)      NOT NULL,
	[record_order]     [int] IDENTITY(1,1) NOT NULL
)
GO

--创建用户信息表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_UserInfo')
DROP TABLE T_UserInfo
GO
CREATE TABLE T_UserInfo
(
	[id]           [nvarchar](200)  PRIMARY KEY  NOT NULL,
	[user_id]      [nvarchar](200)     NOT NULL,
	[user_name]    [nvarchar](200)     NOT NULL,
	[user_pwd]     [nvarchar](200)     NOT NULL,  -- 一定不能使用明文存储！！！
	[user_pid]     [nvarchar](200)     NOT NULL,
	[user_level]   [nvarchar](200)     NOT NULL,
	[user_levelName] [nvarchar](200),
	[person_name]  [nvarchar](50),
	[unit_id]      [nvarchar](200),
	[user_image]   [nvarchar](max),
	[user_QQ]      [int],
	[user_phone]   [nvarchar](200),
	[user_address] [nvarchar](max),
	[user_email]   [nvarchar](100),
	[user_idcard]  [nvarchar](50),
	[is_del]       [bit] DEFAULT 0     NOT NULL,

	[create_time]  [datetime]          NOT NULL,
	[creater_name] [nvarchar](50)      NOT NULL,
	[updated_time] [datetime]          NOT NULL,
	[updater_name] [nvarchar](50)      NOT NULL,
	[record_order] [int] IDENTITY(1,1) NOT NULL
)
GO

--所有图片的相对位置信息表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_ImageInfo')
DROP TABLE T_ImageInfo
GO
CREATE TABLE T_ImageInfo
(
	[id]                [nvarchar](200)   PRIMARY KEY  NOT NULL,
	[image_name]        [nvarchar](200)     NOT NULL,
	[image_url]         [nvarchar](200)     NOT NULL,
	[image_owner]       [nvarchar](200),
	[image_description] [nvarchar](max),
	[is_del]            [bit] DEFAULT 0     NOT NULL,
	[image_type]        [nvarchar](100),
	[image_res]         [nvarchar](100),
	[image_size]        [nvarchar](100),

	[create_time]       [datetime]          NOT NULL,
	[creater_name]      [nvarchar](50)      NOT NULL,
	[updated_time]      [datetime]          NOT NULL,
	[updater_name]      [nvarchar](50)      NOT NULL,
	[record_order]      [int] IDENTITY(1,1) NOT NULL
)
GO

--所有文件的相对位置信息表
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'T_FileInfo')
DROP TABLE T_FileInfo
GO
CREATE TABLE T_FileInfo
(
	[id]               [nvarchar](200) PRIMARY KEY   NOT NULL,
	[file_name]        [nvarchar](200)     NOT NULL,
	[file_url]         [nvarchar](200)     NOT NULL,
	[file_owner]       [nvarchar](200),
	[view_description] [nvarchar](max),
	[is_del]           [bit] DEFAULT 0     NOT NULL,

	[create_time]      [datetime]          NOT NULL,
	[creater_name]     [nvarchar](50)      NOT NULL,
	[updated_time]     [datetime]          NOT NULL,
	[updater_name]     [nvarchar](50)      NOT NULL,
	[record_order]     [int] IDENTITY(1,1) NOT NULL
)
GO


--写入默认信息

CREATE PROCEDURE P_InsertDefaultInfo
	@id NVARCHAR(200),
	@creater_name NVARCHAR(50)
AS
