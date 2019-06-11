CREATE PROCEDURE [dbo].[root_sp_EDIT_Products]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(100),
	@description NVARCHAR(MAX),
	@keywords NVARCHAR(MAX),
	@content NVARCHAR(MAX),
	@isNew BIT,
	@output NVARCHAR(MAX) OUTPUT
AS
	DECLARE
		@product INT,
		@distinct INT
	SET @product = (SELECT COUNT(*) FROM [dbo].[Products] WHERE [Id] = @id)
	SET @distinct = (SELECT COUNT(*) FROM [dbo].[Products] WHERE [Name] = @name)

	BEGIN TRANSACTION
		BEGIN TRY
			IF(@product = 0)
				BEGIN
					IF(@distinct = 0)
						BEGIN
							INSERT [dbo].[Products]
							(
								[Id],
								[Name],
								[Description],
								[Keywords],
								[Content],
								[IsNew],
								[ReferenceId]
							)
							VALUES
							(
								NEWID(), -- Id - uniqueidentifier
								@name,  -- Name - nvarchar(max)
								@name,  -- Description - nvarchar(max)
								@name,  -- Keywords - nvarchar(max)
								@name,  -- Content - nvarchar(max)
								@isNew, -- IsNew - bit
								NULL  -- ReferenceId - uniqueidentifier
							)
							COMMIT
							SELECT @output = N'Ok//Продукт ' + @name + ' успешно добавлен'
						END
					ELSE
						BEGIN
							ROLLBACK
							SELECT @output = N'No//Distinct. Продукт ' + @name + ' уже существует'
						END
				END
			ELSE
				BEGIN
					UPDATE [dbo].[Products] SET
						[Name] = @name,
						[Description] = @description,
						[Keywords] = @keywords,
						[Content] = @content,
						[IsNew] = @isNew
					WHERE Id = @id
					COMMIT
					SELECT @output = N'Ok//Продукт ' + @name + ' успешно изменён'
				END
		END TRY

		BEGIN CATCH
			SELECT @output = N'No//Ошибка №' + CONVERT(NVARCHAR(20), ERROR_NUMBER()) + ': ' + ERROR_MESSAGE()
			ROLLBACK
		END CATCH