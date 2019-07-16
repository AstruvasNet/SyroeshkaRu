CREATE PROCEDURE [dbo].[root_sp_EDIT_History]
	@operationType INT,
	@itemType INT,
	@dateIn INT,
	@itemId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER,
	@output NVARCHAR(MAX) OUTPUT
AS
	DECLARE @id UNIQUEIDENTIFIER

	SET @id = NEWID()

	BEGIN TRY
		IF @@TRANCOUNT > 0
			SAVE TRANSACTION [EDIT_History]
		ELSE
			BEGIN TRANSACTION [EDIT_History]
			SAVE TRANSACTION [Home]
				IF @itemId <> CAST(0x0 AS UNIQUEIDENTIFIER)
					BEGIN
						INSERT [dbo].[History]
						(
							[Id],
							[OperationType],
							[ItemType],
							[DateIn],
							[ItemId],
							[UserId]
						)
						VALUES
						(
							@id,
							@operationType,
							@itemType,
							@dateIn,
							@itemId,
							@userId
						)
						SAVE TRANSACTION [EDIT_History]
					END
				ELSE
					ROLLBACK TRANSACTION [Home]
			COMMIT TRANSACTION [EDIT_History]
		--SELECT @output = N'1//Операция ' + CONVERT(NVARCHAR(MAX), @id) + ' добавлена'
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION [EDIT_History]
		--SELECT @output = N'0//Ошибка №' + CONVERT(NVARCHAR(MAX), ERROR_NUMBER()) + ': ' + ERROR_MESSAGE()
	END CATCH