CREATE PROCEDURE [dbo].[spHost_SelectAll]
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	SELECT * FROM [tblHost];
END
RETURN 0
