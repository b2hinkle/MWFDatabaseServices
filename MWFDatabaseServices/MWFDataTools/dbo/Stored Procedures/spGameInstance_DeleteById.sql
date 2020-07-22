CREATE PROCEDURE [dbo].[spGameInstance_DeleteById]
	@inId int
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	DELETE FROM [tblGameInstance] WHERE Id = @inId;
END
RETURN 0
