CREATE VIEW [dbo].[vwGameInstanceWithHostIp]
	AS SELECT [tblGameInstance].Id, [tblGameInstance].Game, [tblGameInstance].Args, [tblHost].HostIp FROM [tblGameInstance]
	JOIN [tblHost]
	ON [tblGameInstance].HostId = [tblHost].Id;