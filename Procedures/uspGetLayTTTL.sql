﻿--Lấy TTTL
USE QuanLyCaPhe
GO
IF OBJECT_ID('uspGetLayTTTL') IS NOT NULL
DROP PROC uspGetLayTTTL
GO
CREATE PROC uspGetLayTTTL
AS
BEGIN
	select *From TinhLuong

END
--GO
EXEC uspGetLayTTTL