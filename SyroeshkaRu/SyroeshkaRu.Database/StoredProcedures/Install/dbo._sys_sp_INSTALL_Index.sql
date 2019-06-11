CREATE PROCEDURE [dbo].[sys_sp_INSTALL_Index]
	@output NVARCHAR(MAX) OUT
AS
	EXEC dbo.sys_sp_INSTALL_MainMenu @output = @output OUTPUT
		PRINT @output
	EXEC dbo.sys_sp_INSTALL_SecondMenu @output = @output OUTPUT
		PRINT @output
	
	
