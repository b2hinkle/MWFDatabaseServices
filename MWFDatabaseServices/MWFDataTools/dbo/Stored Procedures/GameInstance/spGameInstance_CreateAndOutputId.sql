CREATE PROCEDURE [dbo].[spGameInstance_CreateAndOutputId]
	@inGameInstance udtGameInstance READONLY,
	@outId int OUT	--Our out parameter so we can keep track of it in C# for when we want to destroy it
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	INSERT INTO tblGameInstance([ProcessId], Game, [Port], Args, HostId)
	SELECT [ProcessId], [Game], [Port], [Args], [HostId] FROM @inGameInstance;
	SELECT @outId = SCOPE_IDENTITY();	--SCOPE_IDENTITY() returns the most recent modified Id within the scope of this procedure (last identity created in the same session and the same scope)
END
RETURN 0
