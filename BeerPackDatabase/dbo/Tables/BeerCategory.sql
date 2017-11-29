CREATE TABLE [dbo].[BeerCategory] (
    [CategoryID] NVARCHAR (100) NOT NULL,
    [BeerID]  INT            NOT NULL,
    CONSTRAINT [PK_BeerCategory] PRIMARY KEY CLUSTERED ([BeerID] ASC, [CategoryID] ASC), 
    CONSTRAINT [FK_BeerCategory_Beer] FOREIGN KEY (BeerID) REFERENCES Beer(BeerID), 
    CONSTRAINT [FK_BeerCategory_Category] FOREIGN KEY (CategoryID) REFERENCES Category(Id)
);


