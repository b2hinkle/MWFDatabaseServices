CREATE PROCEDURE [dbo].[spGameInstance_DeleteByHostId]
	@inHostId int,
	@outRowsAffected int OUT	--Use this in C# to see if this sp removed the same amount of game instances the Host has
AS
BEGIN
	SET NOCOUNT OFF;		--We want to know the number of rows removed


	DELETE FROM [tblGameInstance] WHERE HostId = @inHostId;
	

	SELECT @outRowsAffected = @@ROWCOUNT;	
END
RETURN 0
