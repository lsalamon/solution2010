-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		CC
-- Create date: 2010-11-11
-- Description:	ÐÞ¸Ä·Ö×éÅÅÐò
-- =============================================
CREATE PROCEDURE sp_update_group_sort
	-- Add the parameters for the stored procedure here
	@userid int,
	@id		int, 
	@sort	int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE group_cursor CURSOR
	FOR
		SELECT ID 
		FROM Tb_UserGroup
		WHERE UserID=@userid AND ID<>@id AND SortNum>=@sort 
		ORDER BY SortNum
	OPEN group_cursor
	
	DECLARE @tid int, @num int
	SET @num = @sort
	
	FETCH NEXT FROM group_cursor INTO @tid
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		IF (@@FETCH_STATUS <> -2)
		BEGIN    
			SET @num = @num + 1
			UPDATE Tb_UserGroup SET SortNum = @num WHERE ID=@tid
		END
		FETCH NEXT FROM group_cursor INTO @tid
	END
	UPDATE Tb_UserGroup SET SortNum = @sort WHERE ID=@id
	CLOSE group_cursor
	DEALLOCATE group_cursor
END
GO
