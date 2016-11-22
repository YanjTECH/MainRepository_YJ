USE LTMvc
GO 

--新闻信息视图
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'V_News')
DROP VIEW V_News
GO
CREATE VIEW V_News AS
SELECT a.id, a.news_title, a.catalog_id, a.catalog_name, a.news_summary, a.publish_date, a.view_times, a.updater_name, b.news_info 
FROM T_News AS a, T_NewsInfo AS b
WHERE a.id = b.news_id; 

