CREATE PROCEDURE [dbo].[sys_sp_INSTALL_MainMenu]
	@output NVARCHAR(MAX) OUTPUT
AS
	BEGIN
		DECLARE @menuArray TABLE
		(
			[Id] UNIQUEIDENTIFIER NOT NULL,
			[Name] NVARCHAR(50) NOT NULL,
			[Title] NVARCHAR(50) NOT NULL,
			[Type] INT NOT NULL,
			[ParentId] UNIQUEIDENTIFIER NULL,
			[Level] INT NOT NULL
		)

		DECLARE @i INT,
			@menuArrayCount INT,
			@thisName NVARCHAR(50),
			@thisItem INT
		SET @i = 0

		INSERT @menuArray
		(
			[Id], [Name], [Title], [Type], [ParentId], [Level]
		)
		VALUES
		(
			NEWID(), N'site', N'Сайт', 1, NULL, 1
		),
		(
			NEWID(), N'shop', N'Магазин', 1, NULL, 2
		),
		(
			NEWID(), N'reservations', N'Заказы', 1, NULL, 3
		),
		(
			NEWID(), N'messages', N'Сообщения', 1, NULL, 4
		),
		(
			NEWID(), N'root', N'Администрирование', 1, NULL, 5
		)

		SET @menuArrayCount = (SELECT COUNT(Id) FROM @menuArray)

		PRINT N'Массив из ' + CONVERT(NVARCHAR(MAX), @menuArrayCount) + ' элементов успешно создан'

		WHILE @menuArrayCount > 0

			BEGIN TRY
			BEGIN TRANSACTION

				SET @i = @i + 1

				PRINT N'Обработка потока ' + CONVERT(NVARCHAR(10), @i) + ' из ' + CONVERT(NVARCHAR(10), @menuArrayCount)
	
				SET @thisName = (SELECT TOP(1) [Name] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC)
				SET @thisItem = (SELECT COUNT(Id) FROM [dbo].[Menu] WHERE [Name] = @thisName)

				IF(@thisItem = 0)
					BEGIN
						INSERT [dbo].[Menu]
						(
							[Id],
							[Name],
							[Title],
							[Type],
							[ParentId],
							[Level],
							[SequrityId],
							[IsDeleted]
						)
						VALUES
						(
							(SELECT TOP(1) [Id] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Name] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Title] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Type] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [ParentId] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Level] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Id] FROM [dbo].[SequrityProfiles] WHERE [Name] = 'root' ORDER BY [Id] ASC),
							0
						)
				
						COMMIT
						PRINT N'Добавлен пункт меню ' + @thisName
					END
				ELSE
					BEGIN
						PRINT @thisName + N' уже существует'
						ROLLBACK
					END

				IF(@i = @menuArrayCount)
					BREAK
					PRINT N'Добавлено ' + CONVERT(NVARCHAR(10), @i) + ' записей'
					BEGIN
						SELECT @output = N'Все процессы успешно выполнены'
					END

			END TRY

			BEGIN CATCH

				SELECT @output = N'Ошибка №' + CONVERT(NVARCHAR(10), ERROR_NUMBER()) + ': ' + ERROR_MESSAGE()

				ROLLBACK

			END CATCH
	END