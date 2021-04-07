CREATE PROCEDURE [dbo].[spHost_DeleteById]
	@inId int
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	DELETE FROM [tblHost] WHERE Id = @inId;
END
RETURN 0
