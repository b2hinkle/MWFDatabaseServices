CREATE TABLE [dbo].[tblHost]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HostIp] VARCHAR(32) NOT NULL, 
    [HostServicesAPISocketAddress] VARCHAR(40) NOT NULL, 
    [IsActive] BIT NOT NULL
)
