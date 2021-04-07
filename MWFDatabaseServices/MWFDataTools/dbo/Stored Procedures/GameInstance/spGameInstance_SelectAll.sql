
CREATE PROCEDURE [dbo].[spGameInstance_SelectAll]
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	SELECT * FROM [tblGameInstance];
END
RETURN 0
