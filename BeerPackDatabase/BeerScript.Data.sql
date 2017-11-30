/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
insert into Beer ([Name], [Brand], [Beer Style], [Description], Price, Image)values
('Shocktop Belgian White', 'Anheuser-Busch', 'Ale', 'Belgian Wheat Ale', 2, '/images/Ales/shocktop.jpg'),
('Goose Island 312', 'Goose Island Brewing Co.', 'Ale', 'Wheat Ale', 2, '/images/Ales/gooseisland312.png'),
('Samual Adams Summer Ale', 'The Boston Beer Company', 'Ale', 'Summer Ale', 2, '/images/Ales/beer_171.jpg'),
('Bells Best Brown Ale', 'Bells Brewery, inc.', 'Ale', 'Brown Ale' , 2, '/images/Ales/bells-best-brown-ale.jpg'),
('Big Wave', 'Kona Brewing Co.', 'Ale', 'Golden Ale', 2, '/images/Ales/kona-beer.png'),
('Shiner Bock Birthday Beer', 'The Gambrinus Company ', 'Ale', 'Coffee Ale', 2, '/images/Ales/SHiner-793x525.png'),
('Union Jack IPA', 'Firestone Walker Brewing Co.', 'IPA', 'American IPA', 2, '/images/IPAs/firestoneipa.jpg'),
('Goose Island IPA', 'Goose Island Brewing Co.', 'IPA', 'English IPA', 2, '/images/IPAs/gooseislandipa.jpg'),
('Lagunitas IPA', 'Lagunitas Brewing Co.', 'IPA', 'American IPA', 2, '/images/IPAs/Lagunitas.jpg'),
('Samual Adams Rebel IPA', 'The Boston Beer Company', 'IPA', 'American IPA', 2, '/images/IPAs/samualadamsipa.jpg'),
('Sierra Nevada Pale Ale', 'Sierra Nevada Brewing Co.', 'IPA', 'American Pale Ale', 2, '/images/IPAs/sierranevada.jpg'),
('VooDoo Ranger IPA', 'New Belgium Brewing Co.', 'IPA', 'American IPA', 2, '/images/Ipas/voodoo-ranger-by-new-belgium-brewing-co.jpg'),
('LongBoard', 'Kona Brewing Co.', 'Lager', 'Island Lager', 2, '/images/Lagers/beer-longboard-lager.jpg'),
('Hop House 13 Lager', 'Guinness Ltd.', 'Lager', 'Euro Pale Lager', 2, '/images/Lagers/guinness-guinness-hop-house-13-lager_1478111845.png'),
('Eliot Ness Amber Lager', 'Great Lakes Brewing Co.', 'Lager', 'Amber Lager', 2, '/images/Lagers/Lager-Beer-Gear-Patrol-Great-Lakes.jpg'),
('Peroni Nastro Azzurro', 'Birra Peroni Industriale', 'Lager', 'Euro Pale Lager', 2, '/images/Lagers/peroni.jpg'),
('Samual Adams Boston Lager', 'The Boston Beer Company', 'Lager', 'Boston Lager', 2, '/images/Lagers/samual adams.jpg'),
('Yuengling Traditional Lager', 'Yuengling Brewery', 'Lager', 'Amber/Red Lager', 2, '/images/Lagers/yuengling-lager-btl-single.png'),
('REV PILS', 'Revolution Brewing', 'Pilsner', 'German Pilsner', 2, '/images/Pilsners/brewingrevolutionpils .jpg'),
('Pivo Pils', 'Firestone Walker Brewing Co.', 'Pilsner', 'German Pilsner', 2, '/images/Pilsners/FirestonePils.jpg'),
('Pilsner Urquell', 'Plzeňský Prazdroj', 'Pilsner', 'Czech Pilsner', 2, '/images/Pilsners/PilsnerUrquell.jpg'),
('Scrimshaw Pilsner', 'North Coast Brewing Co.', 'Pilsner', 'German Pilsner', 2, '/images/Pilsners/scrimshaw.jpg'),
('Summit Keller Pils', 'Summit Brewing Co.', 'Pilsner', 'German Pilsner', 2, '/images/Pilsners/summit keller pils .jpg'),
('Stammtisch German Style Pilsner', 'Urban Chestnut Brewing Co.', 'Pilsner', 'German Pilsner', 2, '/images/Pilsners/urban chestnut stammtisch .jpg'),
('Founders Porter', 'Founders Brewing Co.', 'Porter', 'American Porter', 2, '/images/Porters/founders.jpg'),
('Dublin Porter', 'Guinness Ltd.', 'Porter', 'English Porter', 2, '/images/Porters/guinness-dublin-porter.jpg'),
('West Indies Porter', 'Guinness Ltd.', 'Porter', 'English Porter', 2, '/images/Porters/guinness-west-indies-porter.jpg'),
('Hofbräu Winter Spezial', 'Hofbräu Winter Spezial', 'Porter', 'German Porter', 2, '/images/Porters/Hofbrau winter spezial.png'),
('Stone Smoked Porter W/Vanilla Bean', 'Stone Brewing', 'Porter', 'Smoked Beer', 2, '/images/Porters/stone-smoked-porter-vanilla-bean.jpg'),
('Yuengling Dark Brewed Porter', 'Yuengling Brewery', 'Porter', 'American Porter', 2, '/images/Porters/Yuengling-Porter.jpg')

delete Category

Insert Into Category (Id)
Values
('Ales'),
('IPAs'),
('Lagers'),
('Pilsners'),
('Porters')
