USE [SYRDB]
GO

CREATE PROCEDURE [system].[sp_DATABASE_Transfer]
	@id INT,
	@name NVARCHAR(255),
	@isNew BIT,
	@output NVARCHAR(MAX) OUTPUT
AS
	DECLARE @newItem UNIQUEIDENTIFIER
	SET @newItem = NEWID()
	BEGIN TRANSACTION
		BEGIN TRY
			INSERT [dbo].[Products]
			(
				[Id],
				[Name],
				[Description],
				[Keywords],
				[Content],
				[IsNew]
			)
			VALUES
			(
				@newItem,
				@name,
				@name,
				@name,
				@name,
				@isNew
			)
			COMMIT
			SELECT @output = N'Добавлен товар ' + @name
		END TRY

		BEGIN CATCH
			SELECT @output = N'Ошибка №' + CONVERT(NVARCHAR(20), ERROR_NUMBER()) + ': ' + ERROR_MESSAGE()
			ROLLBACK
		END CATCH