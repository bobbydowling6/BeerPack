CREATE TABLE [dbo].[OrderProduct]
(
[OrderID] INT NOT NULL,
	[ProductID] INT NOT NULL, 
    CONSTRAINT [PK_OrderProduct] PRIMARY KEY ([ProductID], [OrderID]), 
    CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY (OrderID) REFERENCES [Order](ID), 
    CONSTRAINT [FK_OrderProduct_Beer] FOREIGN KEY (ProductID) REFERENCES Beer(BeerID),
    [Quantity] INT NOT NULL DEFAULT 1, 
	[PlacedName] NVARCHAR(100) NULL,
	[PlacedUnitPrice] MONEY NOT NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GetDate(),
	[DateLastModified] DATETIME NOT NULL DEFAULT GetDate(),

)
