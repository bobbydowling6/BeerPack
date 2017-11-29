CREATE TABLE [dbo].[Beer] (
    [BeerID]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100)   NULL,
    [Brand]          NVARCHAR (100)   NULL,
    [Beer Style]     NVARCHAR (100)   NULL,
    [Description]    NTEXT            NULL,
    [Price]          MONEY            NULL,
    [Image]          NVARCHAR (1000)  NULL,
    [Quantity]       INT              NULL,
    [DateCreated]    DATETIME         DEFAULT (getdate()) NULL,
    [rowguid]        UNIQUEIDENTIFIER DEFAULT (newid()) NULL,
    [NewColumn]      NCHAR (10)       NULL,
    [LastModifiedBy] NVARCHAR (1000)  NULL,
    [IsApproved]     BIT              NULL,
    CONSTRAINT [pk_product] PRIMARY KEY CLUSTERED ([BeerID] ASC)
);


