CREATE TABLE [dbo].[Order]
(
	[Id] INT IDENTITY NOT NULL, 
    [TrackingNumber] CHAR(8) NOT NULL DEFAULT left(newid(), 8), 
    [Email] NVARCHAR(512) NOT NULL, 
    [PurchaserName] NVARCHAR(512) NOT NULL, 
    [ShippingAddress1] NVARCHAR(1000) NOT NULL, 
    [ShippingCity] NVARCHAR(1000) NOT NULL, 
    [ShippingState] NVARCHAR(100) NOT NULL, 
    [ShippingPostalCode] NCHAR(10) NOT NULL, 
    [SubTotal] MONEY NOT NULL, 
    [ShippingAndHandling] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GetDate(), 
    [DateLastModified] DATETIME NOT NULL DEFAULT GetDate(), 
    [ShipDate] DATETIME NULL, 
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id])
)
