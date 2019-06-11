USE SYRDB
GO

CREATE VIEW [dbo].[vw_StoragesMerge]
	AS SELECT
		Storage.Id AS StorageId,
		Storage.Title AS StorageTitle,
		Storage.[Description] AS StorageDescription,
		Product.Id AS ProductId,
		Product.[Name] AS ProdcutName,
		[Merge].Price AS Price,
		[Merge].Quantity AS Quantity
	FROM [dbo].[Storages] AS Storage
	INNER JOIN [dbo].[StoragesProducts] AS [Merge]
	ON [Merge].StorageId = Storage.Id
		INNER JOIN [dbo].[Products] AS Product
	ON Product.Id = [Merge].ProductId
	GROUP BY Storage.Id,
		Storage.Title,
		Storage.[Description],
		Product.Id,
		Product.[Name],
		[Merge].Price,
		[Merge].Quantity