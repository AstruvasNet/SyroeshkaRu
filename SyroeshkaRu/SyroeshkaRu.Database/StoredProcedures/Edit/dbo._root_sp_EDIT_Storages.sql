CREATE PROCEDURE [dbo].[root_sp_EDIT_Storages]
	@id UNIQUEIDENTIFIER,
	@title NVARCHAR(50),
	@description NVARCHAR(MAX) = NULL,
	@isDefault BIT = NULL,
	@dateTime INT,
	@userId UNIQUEIDENTIFIER,
	@flag INT = NULL,
	@isDeleted BIT = 0,
	@output NVARCHAR(MAX) OUTPUT

AS

DECLARE @storage INT,
	@distinct INT,
	@thisStorage UNIQUEIDENTIFIER,
	@operationType INT,
	@itemId UNIQUEIDENTIFIER,
	@newId UNIQUEIDENTIFIER

DECLARE @tmp TABLE(Id UNIQUEIDENTIFIER NOT NULL)

SET @storage = (SELECT COUNT(*) FROM [dbo].[Storages] WHERE [Id] = @id)
SET @distinct = (SELECT COUNT(*) FROM [dbo].[Storages] WHERE [Title] = @title)
SET @thisStorage = (SELECT TOP(1) [Id] FROM [dbo].[Storages] WHERE [Id] = @id AND [Title] = @title ORDER BY [Title])

IF @isDefault IS NULL
	SET @isDefault = (SELECT TOP(1) [IsDefault] FROM [dbo].[Storages] WHERE [Id] = @id ORDER BY [IsDefault])

BEGIN TRY
	BEGIN
	BEGIN TRANSACTION
	SAVE TRANSACTION [Home]
	IF @isDefault = 1
		UPDATE [dbo].[Storages] SET
			[IsDefault] = 0
		WHERE [Id] <> @id
	SAVE TRANSACTION [SetDefaultFalse]
	IF @distinct = 0 OR @thisStorage IS NOT NULL
		IF @storage = 0
			BEGIN
				INSERT @tmp (Id) VALUES (NEWID())

				SET @newId = (SELECT TOP(1) Id FROM @tmp ORDER BY Id)

				INSERT [dbo].[Storages]
				(
					[Id],
					[Title],
					[Description],
					[IsDefault],
					[IsDeleted]
				)
				VALUES
				(
					@newId,
					@title,
					@description,
					@isDefault,
					@isDeleted
				)

				SET @id = @newId
				SET @itemId = @id
				SET @operationType = 0
				SELECT @output = N'1//Склад ' + @title + ' успешно добавлен'
				SAVE TRANSACTION [Insert]
			END
		ELSE
			BEGIN
				UPDATE [dbo].[Storages] SET
					[Title] = @title,
					[Description] = @description,
					[IsDefault] = @isDefault
				WHERE Id = @id

				SET @itemId = @id
				SET @operationType = 1
				SELECT @output = N'1//Склад ' + @title + ' успешно обновлён'
				SAVE TRANSACTION [Update]
			END
	ELSE
		BEGIN
			SELECT @output = N'0//Склад ' + @title + ' уже существует'
			ROLLBACK TRANSACTION [Home]
		END
	IF @flag = 0
		BEGIN
			SET @itemId = @id
			SET @operationType = 3
			DELETE FROM [dbo].[Storages] WHERE [Id] = @id
			SELECT @output = N'1//Склад ' + @title + ' успешно удалён'
			SAVE TRANSACTION [Delete]

			IF @isDefault = 1
				BEGIN
					SET @itemId = NULL
					SELECT @output = N'0//Склад ' + @title + ' невозможно удалить так как он является складом по умолчанию'
					ROLLBACK TRANSACTION [Home]
				END
		END

	COMMIT

	GOTO HISTORY

	END

END TRY

BEGIN CATCH
	SELECT @output = N'0//Ошибка №' + CONVERT(NVARCHAR(20), ERROR_NUMBER()) + ': ' + ERROR_MESSAGE() + '. Строка ' + CONVERT(NVARCHAR(20), ERROR_LINE())
	ROLLBACK
END CATCH

HISTORY:
	EXEC [dbo].[root_sp_EDIT_History]
		@operationType = @operationType,
		@itemType = 0,
		@dateIn = @dateTime,
		@itemId = @itemId,
		@userId = @userId,
		@output = @output OUT