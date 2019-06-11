CREATE PROCEDURE [dbo].[sys_sp_INSTALL_SecondMenu]
	@output NVARCHAR(MAX) OUTPUT
AS
	BEGIN
		DECLARE @menuArray TABLE
		(
			[Id] UNIQUEIDENTIFIER NOT NULL,
			[Name] NVARCHAR(50) NOT NULL,
			[Title] NVARCHAR(50) NOT NULL,
			[Type] INT NOT NULL,
			[Level] INT NOT NULL,
			[ParentLevel] INT NOT NULL,
			[ProcessLevel] INT NOT NULL IDENTITY
		)

		DECLARE @i INT,
			@menuArrayCount INT,
			@thisName NVARCHAR(50),
			@thisItem INT
		SET @i = 0

		INSERT @menuArray
		(
			[Id],
			[Name],
			[Title],
			[Type],
			[Level],
			[ParentLevel]
		)
		VALUES
		(
			NEWID(), N'index', N'Новости', 1, 1, 1
		),
		(
			NEWID(), N'banners', N'Баннеры', 1, 2, 1
		),
		(
			NEWID(), N'pages', N'Страницы', 1, 3, 1
		),
		(
			NEWID(), N'settings', N'Настройки', 1, 4, 1
		),
		(
			NEWID(), N'index', N'Пользователи', 1, 1, 5
		),
		(
			NEWID(), N'storages', N'Склады', 1, 2, 5
		),
		(
			NEWID(), N'products', N'Продукты', 1, 3, 5
		),
		(
			NEWID(), N'sequrity', N'Безопасность', 1, 4, 5
		),
		(
			NEWID(), N'history', N'История', 1, 5, 5
		)

		SET @menuArrayCount = (SELECT COUNT(Id) FROM @menuArray)

		SET @thisName = (SELECT TOP(1) [Name] FROM @menuArray WHERE [Level] = @i ORDER BY [Level] ASC)

		SET @thisItem = (SELECT COUNT(Id) FROM [dbo].[Menu] WHERE [Name] = @thisName)

		PRINT N'Массив из ' + CONVERT(NVARCHAR(MAX), @menuArrayCount) + ' элементов успешно создан'

		WHILE @menuArrayCount > 0
			BEGIN TRY
			BEGIN TRANSACTION
				SET @i = @i + 1
				PRINT N'SecondMenu: Обработка потока ' + CONVERT(NVARCHAR(10), @i) + ' из ' + CONVERT(NVARCHAR(10), @menuArrayCount)
						
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
							(SELECT TOP(1) [Id] FROM @menuArray WHERE [ProcessLevel] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Name] FROM @menuArray WHERE [ProcessLevel] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Title] FROM @menuArray WHERE [ProcessLevel] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Type] FROM @menuArray WHERE [ProcessLevel] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Id] FROM [dbo].[Menu] WHERE [Level] = (SELECT TOP(1) [ParentLevel] FROM @menuArray WHERE [ProcessLevel] = @i ORDER BY [Level] ASC) AND ParentId IS NULL ORDER BY [Level] ASC),
							(SELECT TOP(1) [Level] FROM @menuArray WHERE [ProcessLevel] = @i ORDER BY [Level] ASC),
							(SELECT TOP(1) [Id] FROM [dbo].[SequrityProfiles] WHERE [Name] = 'root' ORDER BY [Id] ASC),
							0
						)

						COMMIT
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