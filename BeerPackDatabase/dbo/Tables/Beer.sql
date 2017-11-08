CREATE TABLE [dbo].[Beer]
(
	[BeerID] INT identity NOT NULL,
	[Name] NVARCHAR(100),
	[Brand] NVARCHAR(100),
	[Beer Style] NVARCHAR(100),
	[Description] ntext,
	[Price] money,
	[Image] NVARCHAR(1000),
	[Quantity] int,
	constraint pk_product primary key(BeerID), 
	constraint fk_beer_ beerstyle foreign key(BeerStyle) references Style([ID])

)
