CREATE PROCEDURE [dbo].[spGameInstance_SelectAll]
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	SELECT * FROM [tblGameInstance];	--SCOPE_IDENTITY() returns the most recent modified Id within the scope of this procedure (last identity created in the same session and the same scope)
END
RETURN 0
