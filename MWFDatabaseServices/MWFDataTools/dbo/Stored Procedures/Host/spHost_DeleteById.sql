CREATE PROCEDURE [dbo].[spHost_DeleteById]
	@inId int
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected



	IF EXISTS (SELECT * FROM [tblHost] WHERE Id = @inId)
	BEGIN;
		DELETE FROM [tblHost] WHERE Id = @inId;
	END
	ELSE
	BEGIN;
	   -- We tried to delete a row that didn't exist
	   THROW 50000, 'Tried deleting row that did not exist', 1;
	END
END
RETURN 0
