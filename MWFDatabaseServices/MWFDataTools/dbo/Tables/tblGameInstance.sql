CREATE TABLE [dbo].[tblGameInstance]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Game] INT NOT NULL, 
    [Args] VARCHAR(64) NOT NULL, 
    [AssociatedHost] VARCHAR(32) NOT NULL
)
