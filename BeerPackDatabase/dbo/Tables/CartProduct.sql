CREATE TABLE [dbo].[CartProduct]
(
CartID uniqueidentifier NOT NULL,
	ProductID int not null, 
    [Quantity] INT NOT NULL DEFAULT 1, 
	[DateCreated] DATETIME NOT NULL DEFAULT GetDate(),
	[DateLastModified] DATETIME NOT NULL DEFAULT GetDate(),
    CONSTRAINT [PK_CartProduct] PRIMARY KEY ([CartID], [ProductID]), 
    CONSTRAINT [FK_CartProduct_Cart] FOREIGN KEY (CartID) REFERENCES Cart(ID), 
    CONSTRAINT [FK_CartProduct_Product] FOREIGN KEY (ProductID) REFERENCES Beer(BeerID)
)
