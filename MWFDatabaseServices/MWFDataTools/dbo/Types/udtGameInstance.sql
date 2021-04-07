CREATE TYPE [dbo].[udtGameInstance] AS TABLE
(
    [ProcessId] INT NOT NULL, 
	[Game] INT NOT NULL, 
    [Port] VARCHAR (5)  NOT NULL,
    [Args] VARCHAR(64) NOT NULL, 
    [HostId] int
)
