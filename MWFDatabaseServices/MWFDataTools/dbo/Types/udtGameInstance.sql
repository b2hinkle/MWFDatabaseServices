﻿CREATE TYPE [dbo].[udtGameInstance] AS TABLE
(
	[Game] INT NOT NULL, 
    [Args] VARCHAR(64) NOT NULL, 
    [HostId] int
)
