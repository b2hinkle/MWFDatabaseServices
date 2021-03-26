CREATE PROCEDURE [dbo].[spGameInstanceWithHost_SelectAll]
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	SELECT * FROM [vwGameInstanceWithHostIp];
END
RETURN 0
