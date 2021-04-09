CREATE TYPE [dbo].[udtHost] AS TABLE
(
	[HostIp] VARCHAR(32) NOT NULL, 
    [HostServicesAPISocketAddress] VARCHAR(40) NOT NULL,
    [IsActive] BIT NOT NULL
)
