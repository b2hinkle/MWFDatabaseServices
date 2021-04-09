CREATE PROCEDURE [dbo].[spGameInstance_DeleteById]
	@inId int
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	DELETE FROM [tblGameInstance] WHERE HostId=@inId;

	IF EXISTS (SELECT * FROM [tblGameInstance] WHERE Id = @inId)
	BEGIN;
		DELETE FROM [tblGameInstance] WHERE Id = @inId;
	END
	ELSE
	BEGIN;
	   -- We tried to delete a row that didn't exist
	   THROW 50000, 'Tried deleting row that did not exist', 1;
	END


	
END
RETURN 0
