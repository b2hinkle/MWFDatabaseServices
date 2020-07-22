CREATE TABLE [dbo].[tblHost]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [HostIp] VARCHAR(32) NOT NULL, 
    [AssociatedHostServicesApiIp] VARCHAR(32) NOT NULL, 
    [AssociatedHostServicesApiPort] VARCHAR(16) NOT NULL, 
    [IsActive] BIT NOT NULL
)
