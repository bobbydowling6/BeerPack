CREATE TABLE [dbo].[BeerStyle]
(
	[StyleID] INT identity NOT NULL,
	[Name] NVARCHAR(100),
	[Style] NVARCHAR(100),
	[Description] ntext,
	constraint pk_productstyle primary key(StyleID),
	--constraint fk_product foreign key([Name], [Description], [BeerStyle])
)
