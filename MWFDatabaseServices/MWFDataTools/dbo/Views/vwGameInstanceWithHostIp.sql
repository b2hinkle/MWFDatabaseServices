CREATE VIEW [dbo].[vwGameInstanceWithHostIp]
	AS SELECT [tblGameInstance].Id, [tblGameInstance].ProcessId, [tblGameInstance].Game, [tblGameInstance].Port, [tblGameInstance].Args, [tblHost].HostIp FROM [tblGameInstance]
	JOIN [tblHost]
	ON [tblGameInstance].HostId = [tblHost].Id;