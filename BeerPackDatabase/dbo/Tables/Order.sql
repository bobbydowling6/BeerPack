CREATE TABLE [dbo].[Order] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [TrackingNumber]      CHAR (8)        DEFAULT (left(newid(),(8))) NOT NULL,
    [Email]               NVARCHAR (512)  NOT NULL,
    [PurchaserName]       NVARCHAR (512)  NOT NULL,
    [ShippingAddress1]    NVARCHAR (1000) NOT NULL,
    [ShippingCity]        NVARCHAR (1000) NOT NULL,
    [ShippingState]       NVARCHAR (100)  NOT NULL,
    [ShippingPostalCode]  NCHAR (10)      NOT NULL,
    [SubTotal]            MONEY           NOT NULL,
    [ShippingAndHandling] MONEY           NOT NULL,
    [Tax]                 MONEY           NOT NULL,
    [DateCreated]         DATETIME        DEFAULT (getdate()) NOT NULL,
    [DateLastModified]    DATETIME        DEFAULT (getdate()) NOT NULL,
    [ShipDate]            DATETIME        NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC)
);


