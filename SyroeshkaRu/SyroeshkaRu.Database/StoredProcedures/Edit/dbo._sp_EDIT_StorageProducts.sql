USE [SYRDB]
GO

CREATE PROCEDURE [dbo].[sp_EDIT_StoragesProducts]
	@productId UNIQUEIDENTIFIER,
	@storageId UNIQUEIDENTIFIER,
	@price DECIMAL(6, 2),
	@quantity DECIMAL(6, 2),
	@output NVARCHAR(MAX) OUTPUT
AS
	DECLARE
		@id INT,
		@productName NVARCHAR(MAX),
		@storageName NVARCHAR(20)

	SET @id = (SELECT COUNT(*) FROM [dbo].[StoragesProducts] WHERE StorageId = @storageId AND ProductId = @productId)
	SET @productName = (SELECT [Name] FROM [dbo].[Products] WHERE Id = @productId)
	SET @storageName = (SELECT Title FROM [dbo].[Storages] WHERE Id = @storageId)

	BEGIN TRANSACTION
		BEGIN TRY
			IF(@id = 0)
				BEGIN
					INSERT [dbo].[StoragesProducts]
					(
						StorageId,
						ProductId,
						Price,
						Quantity
					)
					VALUES
					(
						@storageId,
						@productId,
						@price,
						@quantity
					)
					COMMIT
					SELECT @output = N'Товар ' + @productName
					+ ' успешно добавлен на склад ' + @storageName
					+ ' в количестве ' + CONVERT(NVARCHAR(10), @quantity)
				END
			ELSE
				BEGIN
					COMMIT
					UPDATE [dbo].[StoragesProducts] SET
						Price = @price,
						Quantity = @quantity
					WHERE StorageId = @storageId AND ProductId = @productId
					SELECT @output = N'Товар ' + @productName
						+ ' успешно изменён на складе ' + @storageName
				END
		END TRY

		BEGIN CATCH
			SELECT @output = N'Ошибка №' + CONVERT(NVARCHAR(20), ERROR_NUMBER()) + ': ' + ERROR_MESSAGE()
			ROLLBACK
		END CATCH